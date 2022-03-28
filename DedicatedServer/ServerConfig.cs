using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;

namespace DedicatedServer
{
    internal class ServerConfig
    {
        public SButton toggleKey { get; set; } = SButton.C;
        public int endingDayInterval { get; set; } = 5; // 5 seconds between each ending day
    }
}
