using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Utils
{
    public enum LogLevel
    {
        Information,
        Debug,
        Error,
        Fatal
    }
    public class Logger
    {
        private LogLevel minLogLevel;
        private string logFilePath;

        public Logger(string logFilePath, LogLevel minLogLevel)
        {
            this.logFilePath = logFilePath;
            this.minLogLevel = minLogLevel;
        }

        public void Log(LogLevel level, string message)
        {
            if (level >= minLogLevel)
            {
                string logEntry = $"[{DateTime.Now}] [{level}] {message}";

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(logEntry);
                }
            }
        }
    }
}
