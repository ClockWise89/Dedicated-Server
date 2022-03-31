namespace DedicatedServer
{
    abstract class SleepRoutineState
    {
        protected SleepRoutine _context;

        public void SetRoutineContext(SleepRoutine context) { _context = context; }
        public abstract void Handle();
    }
}



