using StardewValley;
using DedicatedServer.Util;

namespace DedicatedServer
{
    class InvokeSleepState : SleepRoutineState
    {
        public override void Handle()
        {
            InvokeSleep();
            _context.TransitionTo(new IdleState());
        }

        public void InvokeSleep()
        {
            ModEntry.log.Write($"Host is invoking sleep...", Level.Debug);
            // Invoke night
            ModEntry.helper.Reflection.GetMethod(Game1.currentLocation, "startSleep").Invoke();
        }
    }
}



