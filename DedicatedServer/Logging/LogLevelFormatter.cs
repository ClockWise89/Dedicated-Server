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
            switch (logLevel)
            {
                case Level.Debug: return timestamp + " " + "DEBUG: " + text;
                case Level.Verbose: return timestamp + ": " + text;
                case Level.Info: return timestamp + " " + "INFO: " + text;
                case Level.Warning: return timestamp + " " + "***** WARNING *****: " + text;
                case Level.Error: return timestamp + " " + "-+-+-+-+-+-+-+-+- ERROR -+-+-+-+-+-+-+-+-: " + text;
            }

            return timestamp + " " + text;
        }
    }
}
