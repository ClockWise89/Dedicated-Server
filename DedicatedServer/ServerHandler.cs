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

    public sealed class ServerHandler
    {
        internal static ServerConfig config = new ServerConfig();

        public ServerState _serverState;

        public static ServerHandler Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly ServerHandler instance = new ServerHandler();
        }
        private ServerHandler()
        {
            _serverState = new ServerState();
        }

        internal void TurnOnAutoMode() 
        {
            ModEntry.log.Write($" Auto Mode turned on!", Level.Debug);
            _serverState.IsAutoModeEnabled = true;
            _serverState.EndingDayCountdown = config.endingDayInterval;
            GoToSleep();

            
        }

        internal void TurnOffAutoMode() {
            ModEntry.log.Write($" Attempting to turn off Audo Mode...", Level.Debug);

            _serverState.IsAutoModeEnabled = false;
            _serverState.EndingDayCountdown = config.endingDayInterval;
            ModEntry.log.Write($" Auto Mode turned off!", Level.Debug);
        }

        public void NextAction()
        {
            if (!_serverState.IsAutoModeEnabled)
            {
                return;
            }

            ModEntry.log.Write($"Evaluating next step...", Level.Debug);

            if (_serverState.IsPendingGoingToSleep())
            {
                ModEntry.log.Write($"Pending going to sleep...", Level.Debug);
                ContinueGoToSleep();
            }
            else if (_serverState.EndingDayCountdown == 0)
            {
                if (_serverState.HasInvokedSleep)
                {
                    ModEntry.log.Write($"Invoke sleep is already pending...", Level.Debug);
                    return;
                }

                ModEntry.log.Write($"Initiating going to sleep...", Level.Debug);
                GoToSleep();
            } else
            {
                ModEntry.log.Write($"Invoking sleep in...{ _serverState.EndingDayCountdown }", Level.Debug);
                _serverState.EndingDayCountdown -= 1;
            }
        }

        public void OnSaved()
        {
            _serverState.EndingDayCountdown = config.endingDayInterval;
            _serverState.HasInvokedSleep = false;
        }

        public void ContinueGoToSleep()
        {
            if (_serverState.IsPendingBedWarp)
            {
                WarpPlayerToBed();
            }
            else if (_serverState.IsPendingEndingDay)
            {
                InvokeSleep();
            }
        }

        public void GoToSleep()
        {
            // Warp player to Farmhouse
            if (Game1.player.currentLocation.Name == "FarmHouse")
            {
                if (Game1.player.isInBed.Value)
                {
                    ModEntry.log.Write($"{ Game1.player.Name } is already in bed - skipping warp", Level.Debug);
                }
                else
                    WarpPlayerToBed();

                _serverState.IsPendingBedWarp = false;
                _serverState.IsPendingEndingDay = true;
            }
            else
                WarpPlayerToFarmHouse();
        }
        public void WarpPlayerToFarmHouse()
        {
            ModEntry.log.Write($" Attempting to warp { Game1.player.Name } to Farmhouse...", Level.Debug);
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point entrySpot = farmhouse.getEntryLocation();
            Game1.warpFarmer("FarmHouse", entrySpot.X, entrySpot.Y, false);
            _serverState.IsPendingBedWarp = true;
        }

        public void WarpPlayerToBed() 
        {
            ModEntry.log.Write($"Attempting to warp { Game1.player.Name } to bed...", Level.Debug);

            // Warp host to bed
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point bedSpot = farmhouse.GetPlayerBedSpot();
            Game1.warpFarmer("FarmHouse", bedSpot.X, bedSpot.Y, false);
            _serverState.IsPendingBedWarp = false;
            _serverState.IsPendingEndingDay = true;
        }

        public void InvokeSleep()
        {
            ModEntry.log.Write($"Host is invoking sleep...", Level.Debug);
            // Invoke night
            ModEntry.helper.Reflection.GetMethod(Game1.currentLocation, "startSleep").Invoke();
            _serverState.IsPendingEndingDay = false;
            _serverState.HasInvokedSleep = true;
        }
    }
}
