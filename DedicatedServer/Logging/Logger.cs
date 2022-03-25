using System.IO;
using System;
using System.Reflection;

namespace DedicatedServer.Util
{   public class Logger
    {
        private StreamWriter writer;
        private Level defaultLogLevel = Level.Debug;

        ~Logger()
        {
            writer.Close();
        }

        public Logger(string path)
        {
            writer = new StreamWriter(path);
        }

        public void Write(string text, Level priority)
        {
            if (priority < defaultLogLevel) return;

            string formattedText = priority.GetFormatted(text);

            writer.WriteLine(priority.GetFormatted(text));
            writer.Flush();
        }
    }
}
