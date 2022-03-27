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
    internal class PlayerEventHandler
    {
        public PlayerEventHandler()
        {
            // Register for Input events
            RegisterForEvents();
        }
        private void RegisterForEvents()
        {
            ModEntry.helper.Events.Player.Warped += this.OnPlayerWarped;
        }

        /// <summary>
        /// Raised after the current player moves to a new location.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnPlayerWarped(object sender, WarpedEventArgs e)
        {
            ModEntry.log.Write($" { Game1.player.Name } warped from { e.OldLocation.Name } to { e.NewLocation.Name }", Level.Debug);     
        }
    }
}
