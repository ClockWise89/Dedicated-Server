namespace DedicatedServer
{
    abstract class DayRoutineState
    {
        protected DayRoutine _context;

        public void SetContext(DayRoutine context) { _context = context; }
        public abstract void Handle();
    }
}



