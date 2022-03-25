using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace DedicatedServer
{
    internal class GameLoopEventHandler
    {
        private IModHelper _modHelper;
        public GameLoopEventHandler(IModHelper helper)
        {
            _modHelper = helper;

            // Register for Game loop events
            RegisterForEvents();
        }
        private void RegisterForEvents()
        {
            _modHelper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            _modHelper.Events.GameLoop.Saving += this.OnSaving;
            _modHelper.Events.GameLoop.OneSecondUpdateTicked += this.OnOneSecondUpdateTicked;
            _modHelper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            _modHelper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
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

        }

        /// <summary>
        /// Raised before/after the game state is updated, once per second.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnOneSecondUpdateTicked(object sender, OneSecondUpdateTickedEventArgs e)
        {
           
        }

        /// <summary>
        /// Raised after the in-game clock time changes, which happens in intervals of ten in-game minutes.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {

        }

        /// <summary>
        /// Raised before/after the game state is updated (≈60 times per second).
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {

        }
    }
}
