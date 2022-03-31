using System;

namespace DedicatedServer
{
    /// <summary>
    ///  This Interface is the basis of what a Routine is. Each Routine encapsulates the responsibilities of certain user tasks.
    ///  For instance the SleepRoutine encapsulates the responsibility of sending the user to sleep.
    /// </summary>
    public interface IRoutine
    {
        public void SetGameLoopContext(AutoPilotGameLoopContext context);
        public void Update();
        public void RoutineFinished();

        public IRoutine ResetRoutine();
    }
}