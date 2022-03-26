using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DedicatedServer.Util;

namespace DedicatedServer
{

    public sealed class GameInfo
    {
        public ServerState _serverState;

        public static GameInfo Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly GameInfo instance = new GameInfo();
        }
        private GameInfo()
        {
            _serverState = new ServerState();
        }

    }
    public class ServerState
    {
        private Logger _log = Logger.Instance;
        private bool isAutoModeEnabled;
        private int numberOfPlayers;
        private bool isPaused;

        internal ServerConfig config = new ServerConfig();

        public bool GetIsAutoModeEnabled() { return isAutoModeEnabled; }
        public void SetIsAutoModeEnabled(bool enabled) {
            string newValue = enabled ? "on" : "off";
            _log.Write($"Auto mode changed to { newValue }", Level.Info);
            isAutoModeEnabled = enabled; 
        }

    }
    internal class GameState
    {
        
    }

    internal class UserState
    {

    }
}
