using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DedicatedServer.Util;

namespace DedicatedServer
{
    public class ServerState
    {
        private bool isAutoModeEnabled;
        private bool isPendingBedWarp;
        private bool isPendingEndingDay;
        private int endingDayCountdown;
        private bool hasInvokedSleep;

        public bool IsAutoModeEnabled { 
            get { 
                return isAutoModeEnabled; 
            } 
            set {
                isAutoModeEnabled = value;
                string newValue = value ? "on" : "off";
                ModEntry.log.Write($"Auto mode changed to { newValue }", Level.Info);

                if (value == false)
                {
                    isPendingBedWarp = false;
                    isPendingEndingDay = false;
                }
            } 
        }

        public int NumberOfPlayers;
        public bool IsPaused;
        public bool IsPendingBedWarp { 
            get 
            { 
                return this.isPendingBedWarp; 
            } 
            set {
                this.isPendingBedWarp = value;
            } 
        }
        public bool IsPendingEndingDay {
            get
            {
                return this.isPendingEndingDay;
            }
            set
            {
                this.isPendingEndingDay = value;
            }
        }

        internal bool IsPendingGoingToSleep()
        {
            return isPendingBedWarp || isPendingEndingDay;
        }


        public int EndingDayCountdown
        {
            get 
            {
                return endingDayCountdown;
            }
            set
            {
                endingDayCountdown = value;
            }
        }

        public bool HasInvokedSleep
        {
            get
            {
                return hasInvokedSleep;
            }
            set
            {
                hasInvokedSleep = value;
            }
        }
    }
}
