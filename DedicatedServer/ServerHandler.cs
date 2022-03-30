﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using DedicatedServer.Util;
using StardewValley.Locations;
using Microsoft.Xna.Framework;

namespace DedicatedServer
{

    public sealed class ServerHandler
    {
        internal static ServerConfig config = new();

        // DailyRoutineContext (Go outside to check dialogues, click through them, check mailbox, click through them)
        // private readonly SleepRoutineContext _sleepRoutineContext = new(new IdleState());
        // FestivalContext (Check if festival, go to festival, leave festival (maybe if all dudes went to bed))
        private RoutineContext _routineContext;


        private int sleepCooldown = config.endingDayInterval;

        private bool serverIsOn = false;

        public static ServerHandler Instance { get { return Nested.instance; } }

        internal static ServerConfig Config { get => config; set => config = value; }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly ServerHandler instance = new();
        }
        private ServerHandler()
        {
           List<IRoutine> tempList = new List<IRoutine> { new DailyRoutineContext(new GoOutsideState()), new SleepRoutineContext(new GoToSleepState())  };
            _routineContext = new RoutineContext(tempList);
            // Should have it's own state on what to do next. ReadyForSleep? We are not until we have saved! At a festvial should be a state which should delay sleep as well. In a dialogue as well? reading mailbox as well?
            // The Server should have a path of things to do before invoking the going to sleep states. At Wake up -> Warp outside -> Click through dialogues -> Check mailbox -> check game events (festivals) -> All else is done -> GoToSleep
        }

        public void Update()
        {
            if (!serverIsOn)
                return;

            _routineContext.Update();
        }

        //private bool ShouldGoToSleep()
        //{
        //    if (_sleepRoutineContext.GetCurrentState().GetType() != typeof(IdleState))
        //        return false;

        //    if (sleepCooldown > 0)
        //    {
        //        // TODO: This will update even before the save has been executed. Need to listen to OnSaved before initiating this countdown.
        //        ModEntry.log.Write($"Next ending day in { sleepCooldown } seconds...", Level.Debug);
        //        sleepCooldown--;
        //        return false;
        //    }
                
        //    return true;
        //}

        public void ToggleKeyPressed()
        {
            if (serverIsOn)
                TurnOffAutoMode();
            else
                TurnOnAutoMode();
        }

        private void TurnOnAutoMode() 
        {
            serverIsOn = true;
            ModEntry.log.Write($" Auto Mode turned on!", Level.Debug);
            
           // _sleepRoutineContext.TransitionTo(new GoToSleepState());
        }

        private void TurnOffAutoMode() {
            serverIsOn = false;
            _routineContext.ClearQueue();
            //_sleepRoutineContext.TransitionTo(new IdleState());
            ModEntry.log.Write($" Auto Mode turned off!", Level.Debug);
        }
    }
}
