using System.IO;
using System;
using System.Reflection;
using StardewModdingAPI;

namespace DedicatedServer.Util
{  
    public sealed class Logger
    {
        private StreamWriter writer;
        private Level defaultLogLevel = Level.Debug;

        private Logger()
        {
        }

        public void initializeWriter(string path) { writer = new StreamWriter(path); }

        public void Write(string text, Level priority)
        {
            if (priority < defaultLogLevel) return;

            string formattedText = priority.GetFormatted(text);

            writer.WriteLine(priority.GetFormatted(text));
            writer.Flush();
        }

        public static Logger Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Logger instance = new Logger();
        }
    }
}
