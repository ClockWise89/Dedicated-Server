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
using StardewValley.Locations;
using Microsoft.Xna.Framework;

namespace DedicatedServer
{

    public sealed class ServerHandler
    {
        internal static ServerConfig config = new ServerConfig();

        public ServerState _serverState;
        private UserState _userState;

        public static ServerHandler Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly ServerHandler instance = new ServerHandler();
        }
        private ServerHandler()
        {
            _serverState = new ServerState();
            _userState = new UserState();
        }

        public void ToggleAutoMode()
        {
            _serverState.SetIsAutoModeEnabled(!_serverState.GetIsAutoModeEnabled());
            if (_serverState.GetIsAutoModeEnabled())
                TurnOnAutoMode();
            else
                TurnOffAutoMode();
        }

        private void TurnOnAutoMode() 
        {
            // Warp player to Farmhouse
            if (Game1.player.currentLocation.Name == "FarmHouse")
                WarpPlayerToBed();
            else
                WarpPlayerToFarmhouse();
        }
        private void TurnOffAutoMode() { }
        public void WarpPlayerToFarmhouse()
        {
            ModEntry.log.Write($" Attempting to warp { Game1.player.Name } to Farmhouse...", Level.Debug);
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point entrySpot = farmhouse.getEntryLocation();
            Game1.warpFarmer("FarmHouse", entrySpot.X, entrySpot.Y, false);
            _serverState.SetIsPendingBedWarp(true);
        }

        public void WarpPlayerToBed() 
        {
            ModEntry.log.Write($" Attempting to warp { Game1.player.Name } to bed...", Level.Debug);
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point bedSpot = farmhouse.GetPlayerBedSpot();

            // Warp host to bed
            Game1.warpFarmer("FarmHouse", bedSpot.X, bedSpot.Y, false);
            _serverState.SetIsPendingBedWarp(false);

            _serverState.SetIsPendingEndDay(true);
        }

        public void EndDay()
        {
            ModEntry.log.Write($"Host is invoking sleep...", Level.Debug);
            // Invoke night
            ModEntry.helper.Reflection.GetMethod(Game1.currentLocation, "startSleep").Invoke();
            Game1.displayHUD = true;
            _serverState.SetIsPendingEndDay(false);
        }
    }
    public class ServerState
    {
        private bool isAutoModeEnabled;
        private int numberOfPlayers;
        private bool isPaused;
        private bool isPendingBedWarp = false;
        private bool isPendingEndDay = false;

        public bool GetIsAutoModeEnabled() { return isAutoModeEnabled; }
        public void SetIsAutoModeEnabled(bool enabled) {
            string newValue = enabled ? "on" : "off";
            ModEntry.log.Write($"Auto mode changed to { newValue }", Level.Info);
            isAutoModeEnabled = enabled; 
        }

        public bool GetIsPendingBedWarp() { return isPendingBedWarp; }
        public void SetIsPendingBedWarp(bool value) { isPendingBedWarp = value; }

        public bool GetIsPendingEndDay() { return isPendingEndDay; }
        public void SetIsPendingEndDay(bool value) { isPendingEndDay = value; }
    }

    internal class UserState
    {
        internal int _farmingLevel;
        internal int _miningLevel;
        internal int _foragingLevel;
        internal int _fishingLevel;
        internal int _combatLevel;
    }
}
