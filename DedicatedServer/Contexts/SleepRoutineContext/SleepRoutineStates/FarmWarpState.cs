using StardewValley;
using DedicatedServer.Util;
using StardewValley.Locations;
using Microsoft.Xna.Framework;

namespace DedicatedServer
{
    class FarmWarpState : SleepRoutineState
    {
        public override void Handle()
        {
            // Make sure we are inside the FarmHouse
            if (Game1.player.currentLocation.Name != "FarmHouse")
                WarpPlayerToFarmHouse();
               
            _context.TransitionTo(new BedWarpState());
        }

        public void WarpPlayerToFarmHouse()
        {
            ModEntry.log.Write($" Attempting to warp { Game1.player.Name } to FarmHouse...", Level.Debug);
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point entrySpot = farmhouse.getEntryLocation();
            Game1.warpFarmer("FarmHouse", entrySpot.X, entrySpot.Y, false);
            //_serverState.IsPendingBedWarp = true;
        }
    }
}



