using System.IO;
using System;
using System.Reflection;

namespace DedicatedServer.Util
{   public class Logger
    {
        private StreamWriter writer;
        private Level defaultLogLevel = Level.Verbose;

        public Logger()
        {
            writer = new StreamWriter(GetLocalPath());
        }

        public void Write(string text, Level? priority = null)
        {
            if (priority == null) { priority = defaultLogLevel; }

            string formattedText = priority?.GetFormatted(text);

            if (priority.Equals(Level.Debug)) { 
                Console.WriteLine(formattedText);
                return;
            }

            writer.WriteLine(priority?.GetFormatted(text));
            writer.Flush();
        }

        private string GetLocalPath()
        {
            return Path.Combine((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath.Split(new string[] { "DedicatedServer.dll" }, StringSplitOptions.None)[0], "data\\log.txt");
        }
    }
}
