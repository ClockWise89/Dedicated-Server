using System;

namespace DedicatedServer
{
    public interface IRoutine
    {
        public void SetGameLoopContext(AutoPilotGameLoopContext context);
        public void Update();
        public void RoutineFinished();

        public IRoutine ResetRoutine();
    }
}