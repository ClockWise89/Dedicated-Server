using DedicatedServer.Util;

namespace DedicatedServer
{
    class GoOutsideState : DayRoutineState
    {
        public override void Handle()
        {
            ModEntry.log.Write($"Doing some work in { this.GetType().Name }", Level.Debug);
            _context.TransitionTo(new GoInsideState());
        }
    }
}



