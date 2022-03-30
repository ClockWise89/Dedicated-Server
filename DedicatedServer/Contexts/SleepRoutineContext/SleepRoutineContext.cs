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
    public interface IRoutine
    {
        public void SetContext(RoutineContext context);
        public void Update();
        public void RoutineFinished();
    }
    public class RoutineContext
    {
        private IRoutine _currentRoutine;
        private readonly List<IRoutine> _routineSequence;
        private Queue<IRoutine> _runningRoutineQueue = new();

        public RoutineContext(List<IRoutine> routines)
        {
            _routineSequence = routines;
            foreach (IRoutine routine in _routineSequence)
            {
                routine.SetContext(this);
            }

            ResetQueueBackToSequence();
        }

        private void ResetQueueBackToSequence()
        {
            _runningRoutineQueue = new Queue<IRoutine>(_routineSequence);
            _currentRoutine = _runningRoutineQueue.Dequeue();
        }

        public void Update() 
        {
            _currentRoutine.Update();
        }

        public void NextRoutine()
        {
            IRoutine currentRoutine = _currentRoutine;
            if (!_runningRoutineQueue.TryDequeue(out _currentRoutine))
            {
                ModEntry.log.Write($"Routine cycle finished. Starting over...", Level.Debug);
                ResetQueueBackToSequence();
            } 
            else
            {
                ModEntry.log.Write($"Going from { currentRoutine.GetType().Name } to { _currentRoutine.GetType().Name }", Level.Debug);
                _currentRoutine.SetContext(this);
            }
        }
        public void ClearQueue() { _runningRoutineQueue.Clear(); }
    }

    abstract class DailyRoutineState
    {
        protected DailyRoutineContext _context;

        public void SetContext(DailyRoutineContext context) { _context = context; }
        public abstract void Handle();
    }

    class GoOutsideState : DailyRoutineState
    {
        public override void Handle()
        {
            _context.TransitionTo(new GoInsideState());
        }
    }

    class GoInsideState : DailyRoutineState
    {
        public override void Handle()
        {
            _context.RoutineFinished();
        }
    }

    internal class DailyRoutineContext : IRoutine
    {
        private RoutineContext _context;
        private DailyRoutineState _state;

        public DailyRoutineContext(DailyRoutineState state)
        {
            _state = state;
            _state.SetContext(this);
        }
        public void Update() 
        {
            _state.Handle();
        }

        public void TransitionTo(DailyRoutineState state)
        {
            ModEntry.log.Write($"Going from { _state.GetType().Name } to { state.GetType().Name }", Level.Debug);
            _state = state;
            _state.SetContext(this);
        }

        public void RoutineFinished()
        {
            _context.NextRoutine();
        }

        public void SetContext(RoutineContext context)
        {
            _context = context;
        }
    }
    internal class SleepRoutineContext : IRoutine
    {
        private RoutineContext _context;
        private SleepRoutineState _state = null;
        public SleepRoutineContext(SleepRoutineState state)
        {
            _state = state;
            _state.SetContext(this);
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
        
        public void RoutineFinished()
        {
            _context.NextRoutine();
        }
        public void SetContext(RoutineContext context)
        {
            _context = context;
        }

        public SleepRoutineState GetCurrentState() { return _state; }
    }
}



