namespace DedicatedServer
{
    /// <summary>
    ///  Encapsulates the steps the user needs to perform in order to send the user to sleep. It consists of various different states which encapsulates certain
    ///  smaller game logic tasks, and which upon completion transitions to the next state in the Routine.
    /// </summary>
    abstract class SleepRoutineState
    {
        protected SleepRoutine _context;

        public void SetRoutineContext(SleepRoutine context) { _context = context; }
        public abstract void Handle();
    }
}



