﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using DedicatedServer.Util;


namespace DedicatedServer
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private Logger log;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            string logPath = Path.Combine(this.Helper.DirectoryPath, "data", "log.txt");
            log = new Logger(path: logPath);
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
           // if (!Context.IsWorldReady)
            //    return;

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
            this.log.Write($"{e.Button}");
            this.log.Write($"{e.Button}", Level.Debug);
            this.log.Write($"{e.Button}", Level.Info);
            this.log.Write($"{e.Button}", Level.Warning);
            this.log.Write($"{e.Button}", Level.Error);

        }
    }
}