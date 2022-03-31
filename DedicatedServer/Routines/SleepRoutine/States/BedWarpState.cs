using StardewValley;
using DedicatedServer.Util;
using StardewValley.Locations;
using Microsoft.Xna.Framework;

namespace DedicatedServer
{
    class BedWarpState : SleepRoutineState
    {
        public override void Handle()
        {
            // Make sure we are in bed
            if (!Game1.player.isInBed.Value)
                WarpPlayerToBed();

            _context.TransitionTo(new InvokeSleepState());
        }

        public static void WarpPlayerToBed()
        {
            ModEntry.log.Write($"Attempting to warp { Game1.player.Name } to bed...", Level.Debug);

            // Warp host to bed
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");
            Point bedSpot = farmhouse.GetPlayerBedSpot();
            Game1.warpFarmer("FarmHouse", bedSpot.X, bedSpot.Y, false);
        }
    }
}



