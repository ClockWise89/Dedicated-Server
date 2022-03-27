using System.IO;
using System;
using System.Reflection;
using StardewModdingAPI;

namespace DedicatedServer.Util
{  
    public sealed class Logger
    {
        private StreamWriter _writer;
        private Level defaultLogLevel = Level.Debug;

        private Logger() { }

        public void initializeWriter(string path) {
            _writer = new StreamWriter(path);

            Write("Initializing Logger...", Level.Debug);
        }

        public void Write(string text, Level priority)
        {
            if (priority < defaultLogLevel) return;

            string formattedText = priority.GetFormatted(text);

            _writer.WriteLine(priority.GetFormatted(text));
            ModEntry.monitor.Log(priority.GetFormatted(text), LogLevel.Debug);

            _writer.Flush();
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
