using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using DedicatedServer.Util;
using DedicatedServer.EventHandlers;

namespace DedicatedServer
{

    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        internal static IModHelper helper;
        internal static IMonitor monitor;

        internal static Logger log = Logger.Instance;


        private GameLoopEventHandler _gameLoopHandler;
        private InputEventHandler _inputHandler;
        private PlayerEventHandler _playerHandler;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            ModEntry.helper = helper;
            ModEntry.monitor = Monitor;

            string logPath = Path.Combine(this.Helper.DirectoryPath, "data", "log.txt");
            log.initializeWriter(logPath);

            log.Write("Setting up EventHandlers...", Level.Debug);
            _gameLoopHandler = new GameLoopEventHandler();
            _inputHandler = new InputEventHandler();
            _playerHandler = new PlayerEventHandler();
        }
    }
}