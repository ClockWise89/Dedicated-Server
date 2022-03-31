using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;

namespace DedicatedServer
{
    internal static class ServerConfig
    {
        public static SButton ToggleKey { get; set; } = SButton.C;
        public static int RoutineCycleCooldown { get; set; } = 5; // 5 seconds between each re-iteration of game cycle
    }
}
