namespace DedicatedServer
{
    class GoToSleepState : SleepRoutineState
    {
        public override void Handle()
        {
            _context.TransitionTo(new FarmWarpState());
        }
    }
}



