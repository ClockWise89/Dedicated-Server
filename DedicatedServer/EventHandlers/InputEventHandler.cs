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

namespace DedicatedServer.EventHandlers
{
    internal class InputEventHandler
    {
        private IModHelper _modHelper;
        private IMonitor _monitor;
        private Logger _log = Logger.Instance;

        public InputEventHandler(IModHelper helper, IMonitor monitor)
        {
            _modHelper = helper;
            _monitor = monitor;
            // Register for Input events
            RegisterForEvents();
        }
        private void RegisterForEvents()
        {
            _modHelper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /// <summary>
        /// Raised after the player presses a button on the keyboard, controller, or mouse.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            // if (!Context.IsWorldReady)
            //    return;

            
            // print button presses to the console window
            //_monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);           
        }
    }
}