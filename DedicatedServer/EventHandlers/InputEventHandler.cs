using System;
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

        public InputEventHandler()
        {
            // Register for Input events
            RegisterForEvents();
        }
        private void RegisterForEvents()
        {
            ModEntry.helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /// <summary>
        /// Raised after the player presses a button on the keyboard, controller, or mouse.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (e.Button == ServerHandler.config.toggleKey)
            {
                if (ServerHandler.Instance._serverState.IsAutoModeEnabled)
                    ServerHandler.Instance.TurnOffAutoMode();
                else
                    ServerHandler.Instance.TurnOnAutoMode();
            }

            ModEntry.log.Write($" { Game1.player.Name } pressed { e.Button }", Level.Debug);
        }
    }
}
