namespace DedicatedServer
{
    abstract class SleepRoutineState
    {
        protected SleepRoutineContext _context;

        public void SetContext(SleepRoutineContext context) { _context = context; }
        public abstract void Handle();
    }
}



