using DedicatedServer.Util;

namespace DedicatedServer
{
    internal class DayRoutine : IRoutine
    {
        private AutoPilotGameLoopContext _context;
        private DayRoutineState _state;

        public DayRoutine(DayRoutineState state)
        {
            _state = state;
            _state.SetContext(this);
        }
        public void Update() 
        {
            _state.Handle();
        }

        public void TransitionTo(DayRoutineState state)
        {
            ModEntry.log.Write($"Going from { _state.GetType().Name } to { state.GetType().Name }", Level.Debug);
            _state = state;
            _state.SetContext(this);
        }

        public void RoutineFinished()
        {
            _context.NextRoutine();
        }

        public void SetGameLoopContext(AutoPilotGameLoopContext context)
        {
            _context = context;
        }

        public IRoutine ResetRoutine()
        {
            DayRoutine copy = new(new GoOutsideState());
            copy._context = this._context;
            return copy;
        }
    }
}



