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
        private readonly AutoPilotGameLoopContext _routineContext;
        private bool serverIsOn = false;

        public static ServerHandler Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly ServerHandler instance = new();
        }
        private ServerHandler()
        {
            List<IRoutine> tempList = new() { new DayRoutine(new GoOutsideState()), new SleepRoutine(new GoToSleepState()) };
            _routineContext = new AutoPilotGameLoopContext(tempList);
        }

        public void OnSaved()
        {
            _routineContext.ResetQueueBackToSequence();
        }

        public void Update()
        {
            if (!serverIsOn)
                return;

            _routineContext.Update();
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
        }

        private void TurnOffAutoMode() {
            serverIsOn = false;
            _routineContext.ClearQueue();
            ModEntry.log.Write($" Auto Mode turned off!", Level.Debug);
        }
    }
}
