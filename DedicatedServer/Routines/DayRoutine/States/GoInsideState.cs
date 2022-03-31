using DedicatedServer.Util;

namespace DedicatedServer
{
    class GoInsideState : DayRoutineState
    {
        public override void Handle()
        {
            ModEntry.log.Write($"Doing some work in { this.GetType().Name }", Level.Debug);
            _context.RoutineFinished();
        }
    }
}



