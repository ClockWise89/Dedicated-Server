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
        private Logger _log = Logger.Instance;
        private GameLoopEventHandler _gameLoopHandler;
        private InputEventHandler _inputHandler;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            string logPath = Path.Combine(this.Helper.DirectoryPath, "data", "log.txt");
            _log.initializeWriter(logPath, this.Monitor);

            _log.Write("Setting up EventHandlers...", Level.Debug);

            _gameLoopHandler = new GameLoopEventHandler(helper);
            _inputHandler = new InputEventHandler(helper, this.Monitor);
        }
    }
}