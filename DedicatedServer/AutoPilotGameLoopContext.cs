using System.Collections.Generic;
using DedicatedServer.Util;

namespace DedicatedServer
{
    /// <summary>
    ///  This class handles the sequence of the AutoPilotLoop, i.e. the order in which certain routines should be performed.
    /// </summary>
    public class AutoPilotGameLoopContext
    {
        private IRoutine _currentRoutine;
        private List<IRoutine> _routineSequence;
        private Queue<IRoutine> _runningRoutineQueue = new();
        private int _routineCycleCooldown = 0;

        public AutoPilotGameLoopContext(List<IRoutine> routines)
        {
            _routineSequence = routines;
            foreach (IRoutine routine in _routineSequence)
            {
                routine.SetGameLoopContext(this);
            }

            ResetQueueBackToSequence();
        }

        public void ResetQueueBackToSequence()
        {
            // Make sure we reset with new copies of Routines, and not the state traveresed object. C# uses shallow copies by default!
            List<IRoutine> copiedRoutineSequence = _routineSequence.ConvertAll(routine => routine.ResetRoutine());
            _runningRoutineQueue = new Queue<IRoutine>(copiedRoutineSequence);
            _currentRoutine = _runningRoutineQueue.Dequeue();
            _routineCycleCooldown = ServerConfig.RoutineCycleCooldown;
        }

        public void Update() 
        {
            if (_currentRoutine == null)
                return;

            if (_routineCycleCooldown == 0)
                _currentRoutine.Update();
            else
            {
                ModEntry.log.Write($"Starting over in...{ _routineCycleCooldown }", Level.Debug);
                _routineCycleCooldown--;
            }
        }

        public void NextRoutine()
        {
            IRoutine currentRoutine = _currentRoutine;
            if (!_runningRoutineQueue.TryDequeue(out _currentRoutine))
            {
                ModEntry.log.Write($"Routine cycle finished.", Level.Debug);
            } 
            else
            {
                ModEntry.log.Write($"Going from { currentRoutine.GetType().Name } to { _currentRoutine.GetType().Name }", Level.Debug);
                _currentRoutine.SetGameLoopContext(this);
            }
        }
        public void ClearQueue() { _runningRoutineQueue.Clear(); }
    }
}



