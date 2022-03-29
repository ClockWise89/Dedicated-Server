using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using DedicatedServer.Util;

namespace DedicatedServer
{
    internal class SleepRoutineContext
    {
        private SleepRoutineState _state = null;
        public SleepRoutineContext(SleepRoutineState state)
        {
            _state = state;
        }
        public void TransitionTo(SleepRoutineState state)
        {
            ModEntry.log.Write($"Going from { _state.GetType().Name } to { state.GetType().Name }", Level.Debug);
            _state = state;
            _state.SetContext(this);
        }

        public void Update()
        {
            _state.Handle();
        }

        public SleepRoutineState GetCurrentState() { return _state; }
    }
}



