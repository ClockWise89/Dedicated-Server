using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServer.Util
{
    public enum Level { Debug = 0, Verbose = 1, Info = 2, Warning = 3, Error = 4 }

    static class LogLevelFormatter
    {

        private static string GetTimeStamp(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        }

        public static String GetFormatted(this Level logLevel, string text)
        {
            string timestamp = GetTimeStamp(DateTime.Now);
            string logText = timestamp + " ";
            string logPrefix = "";
            switch (logLevel)
            {
                case Level.Info: logPrefix = "[ INFO  ] "; break;
                case Level.Warning: logPrefix = "[WARNING] "; break;
                case Level.Error: logPrefix = "[ ERROR ] "; break;
            }

            return logText + logPrefix + text;
        }
    }
}
