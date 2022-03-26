using System.IO;
using System;
using System.Reflection;
using StardewModdingAPI;

namespace DedicatedServer.Util
{  
    public sealed class Logger
    {
        private StreamWriter _writer;
        private IMonitor _monitor;

        private Level defaultLogLevel = Level.Debug;

        private Logger() { }

        public void initializeWriter(string path, IMonitor monitor = null) {
            _writer = new StreamWriter(path);
            _monitor = monitor;

            Write("Initializing Logger...", Level.Debug);
        }

        public void Write(string text, Level priority)
        {
            if (priority < defaultLogLevel) return;

            string formattedText = priority.GetFormatted(text);

            _writer.WriteLine(priority.GetFormatted(text));
            if (_monitor != null)
                _monitor.Log(priority.GetFormatted(text), LogLevel.Debug);
 

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
