using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

using System.IO;
using System.Reflection;

namespace DedicatedServer
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private Logger log = new Logger();


        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
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
            this.log.Write($"{e.Button}", Level.Debug);
        }
    }

    public class Logger
    {


        private StreamWriter writer;

        public Logger()
        {
            writer = new StreamWriter(GetLocalPath());
        }

        public void Write(string text, Level priority)
        {
            writer.WriteLine(priority.GetFormatted(text));
            writer.Flush();
        }

        private string GetLocalPath()
        {
            return Path.Combine((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath.Split(new string[] { "DedicatedServer.dll" }, StringSplitOptions.None)[0], "data\\log.txt");
        }
    }

    public enum Level { Debug = 0, Info = 1, Warning = 2, Error = 3 }

    static class LogLevelFormatter
    {

        private static string GetTimeStamp(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        }

        public static String GetFormatted(this Level logLevel, string text)
        {
            string timestamp = GetTimeStamp(DateTime.Now);
            switch (logLevel)
            {
                case Level.Debug: return timestamp + " " + "DEBUG: " + text;
                case Level.Info: return timestamp + " " + "Info: " + text;
                case Level.Warning: return timestamp + " " + "WARNING: " + text;
                case Level.Error: return timestamp + " " + "ERROR: " + text;
            }

            return timestamp + " " + text;
        }
    }
}