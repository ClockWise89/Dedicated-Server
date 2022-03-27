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
    internal class GameLoopEventHandler
    {

        public GameLoopEventHandler()
        {
            // Register for Game loop events
            RegisterForEvents();
        }
        private void RegisterForEvents()
        {
            ModEntry.helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            ModEntry.helper.Events.GameLoop.Saving += this.OnSaving;
            ModEntry.helper.Events.GameLoop.OneSecondUpdateTicked += this.OnOneSecondUpdateTicked;
            ModEntry.helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            ModEntry.helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        /*********
        ** Saving listeners
        *********/

        /// <summary>
        /// Raised after loading a save (including the first day after creating a new save), or connecting to a multiplayer world. 
        /// This happens right before DayStarted; at this point the save file is read and Context.IsWorldReady is true.
        /// This event isn't raised after saving; if you want to do something at the start of each day, see DayStarted instead.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event data.</param>
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            ModEntry.log.Write($"OnSaveLoaded event received", Level.Debug);
            // Do stuff 
        }

        /// <summary>
        /// Raised before/after the game writes data to save file (except the initial save creation). 
        /// The save won't be written until all mods have finished handling this event. This is also raised for farmhands in multiplayer.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event data.</param>
        private void OnSaving(object sender, SavingEventArgs e)
        {
            ModEntry.log.Write($"OnSaving event received", Level.Debug);
        }

        /// <summary>
        /// Raised before/after the game state is updated, once per second.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnOneSecondUpdateTicked(object sender, OneSecondUpdateTickedEventArgs e)
        {
            if (ServerHandler.Instance._serverState.GetIsPendingBedWarp())
                ServerHandler.Instance.WarpPlayerToBed();
            else if (ServerHandler.Instance._serverState.GetIsPendingEndDay())
                ServerHandler.Instance.EndDay();
            // ModEntry.log.Write($"OnOneSecondUpdateTicked event received", Level.Debug);
        }

        /// <summary>
        /// Raised after the in-game clock time changes, which happens in intervals of ten in-game minutes.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {
            ModEntry.log.Write($"OnTimeChanged event received", Level.Debug);
        }

        /// <summary>
        /// Raised before/after the game state is updated (≈60 times per second).
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            // ModEntry.log.Write($"OnUpdateTicked event received", Level.Debug);
        }
    }
}
