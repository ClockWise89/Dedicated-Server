using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using DedicatedServer.Util;
using StardewValley.Locations;
using Microsoft.Xna.Framework;

namespace DedicatedServer
{
    internal class GoingToSleepContext
    {
        private GoingToSleepState _state = null;
        public GoingToSleepContext(GoingToSleepState state)
        {
            _state = state;
        }
        public void TransitionTo(GoingToSleepState state)
        {
            ModEntry.log.Write($"Going from { _state.GetType().Name } to { state.GetType().Name }", Level.Debug);
            _state = state;
            _state.SetContext(this);
        }

        public void Update()
        {
            _state.Handle();
        }

        public GoingToSleepState GetCurrentState() { return _state; }
    }

    abstract class GoingToSleepState
    {
        protected GoingToSleepContext _context;

        public void SetContext(GoingToSleepContext context) { _context = context; }
        public abstract void Handle();
    }

    class IdleState : GoingToSleepState
    {
        public override void Handle()
        {
            // Reset potential variables maybe
        }
    }

    class GoToSleepState : GoingToSleepState
    {
        public override void Handle()
        {
            _context.TransitionTo(new FarmWarpState());
        }
    }

    class FarmWarpState: GoingToSleepState
    {
        public override void Handle()
        {
            // Make sure we are inside the FarmHouse
            if (Game1.player.currentLocation.Name != "FarmHouse")
                WarpPlayerToFarmHouse();
               
            _context.TransitionTo(new BedWarpState());
        }

        public void WarpPlayerToFarmHouse()
        {
            ModEntry.log.Write($" Attempting to warp { Game1.player.Name } to FarmHouse...", Level.Debug);
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point entrySpot = farmhouse.getEntryLocation();
            Game1.warpFarmer("FarmHouse", entrySpot.X, entrySpot.Y, false);
            //_serverState.IsPendingBedWarp = true;
        }
    }

    class BedWarpState : GoingToSleepState
    {
        public override void Handle()
        {
            // Make sure we are in bed
            if (!Game1.player.isInBed.Value)
                WarpPlayerToBed();

            _context.TransitionTo(new InvokeSleepState());
        }

        public void WarpPlayerToBed()
        {
            ModEntry.log.Write($"Attempting to warp { Game1.player.Name } to bed...", Level.Debug);

            // Warp host to bed
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point bedSpot = farmhouse.GetPlayerBedSpot();
            Game1.warpFarmer("FarmHouse", bedSpot.X, bedSpot.Y, false);
          //  _serverState.IsPendingBedWarp = false;
          //  _serverState.IsPendingEndingDay = true;
        }
    }

    class InvokeSleepState : GoingToSleepState
    {
        public override void Handle()
        {
            InvokeSleep();
            _context.TransitionTo(new IdleState());
        }

        public void InvokeSleep()
        {
            ModEntry.log.Write($"Host is invoking sleep...", Level.Debug);
            // Invoke night
            ModEntry.helper.Reflection.GetMethod(Game1.currentLocation, "startSleep").Invoke();
        }
    }

    class EndDayCooldownState : GoingToSleepState
    {
        private int counter = 5;
        public override void Handle()
        {
            if (counter > 0)
            {
                ModEntry.log.Write($"Next ending day in { counter } seconds...", Level.Debug);
                counter--;
                return;
            }

            _context.TransitionTo(new FarmWarpState());
        }
    }
}



