using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using DedicatedServer.Util;

namespace DedicatedServer
{
    internal class SleepRoutine : IRoutine
    {
        private AutoPilotGameLoopContext _context;
        private SleepRoutineState _state = null;
        public SleepRoutine(SleepRoutineState state)
        {
            _state = state;
            _state.SetRoutineContext(this);
        }
        public void TransitionTo(SleepRoutineState state)
        {
            ModEntry.log.Write($"Going from { _state.GetType().Name } to { state.GetType().Name }", Level.Debug);
            _state = state;
            _state.SetRoutineContext(this);
        }

        public void Update()
        {
            _state.Handle();
        }
        
        public void RoutineFinished()
        {
            _context.NextRoutine();
        }
        public void SetGameLoopContext(AutoPilotGameLoopContext context)
        {
            _context = context;
        }

        public SleepRoutineState GetCurrentState() { return _state; }

        public IRoutine ResetRoutine()
        {
            SleepRoutine copy = new(new GoToSleepState());
            copy._context = this._context;
            return copy;
        }
    }
}