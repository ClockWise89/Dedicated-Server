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
        private GoingToSleepContext _goingToSleepContext = new(new IdleState());

        private int sleepCooldown = config.endingDayInterval;

        private bool serverIsOn = false;

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
            // Should have it's own state on what to do next. ReadyForSleep? We are not until we have saved! At a festvial should be a state which should delay sleep as well. In a dialogue as well? reading mailbox as well?
            // The Server should have a path of things to do before invoking the going to sleep states. At Wake up -> Warp outside -> Click through dialogues -> Check mailbox -> check game events (festivals) -> All else is done -> GoToSleep
        }

        public void Update()
        {
            if (!serverIsOn)
                return;

            _goingToSleepContext.Update();
            if (ShouldGoToSleep())
            {
                _goingToSleepContext.TransitionTo(new GoToSleepState());
                sleepCooldown = config.endingDayInterval;
            }
        }

        private bool ShouldGoToSleep()
        {
            if (_goingToSleepContext.GetCurrentState().GetType() != typeof(IdleState))
                return false;

            if (sleepCooldown > 0)
            {
                // TODO: This will update even before the save has been executed. Need to listen to OnSaved before initiating this countdown.
                ModEntry.log.Write($"Next ending day in { sleepCooldown } seconds...", Level.Debug);
                sleepCooldown--;
                return false;
            }
                
            return true;
        }

        public void ToggleKeyPressed()
        {
            if (serverIsOn)
                TurnOffAutoMode();
            else
                TurnOnAutoMode();
        }

        private void TurnOnAutoMode() 
        {
            serverIsOn = true;
            ModEntry.log.Write($" Auto Mode turned on!", Level.Debug);
            _goingToSleepContext.TransitionTo(new GoToSleepState());
        }

        private void TurnOffAutoMode() {
            serverIsOn = false;
            _goingToSleepContext.TransitionTo(new IdleState());
            ModEntry.log.Write($" Auto Mode turned off!", Level.Debug);
        }
    }
}
