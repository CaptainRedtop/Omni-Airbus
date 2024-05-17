namespace Omni_Airbus.Utils.Logger
{

    /// <summary>
    /// Logger class for writing to a log file.
    /// </summary>
    public class Logger
    {
        private LoggerEnum minLogLevel;
        private string logFilePath = Directory.GetCurrentDirectory() + "/log.txt";

        /// <summary>
        /// Logger constructor
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <param name="minLogLevel"></param>
        internal Logger(LoggerEnum minLogLevel)
        {
            this.minLogLevel = minLogLevel;
        }

        /// <summary>
        /// Method for writing to file.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private void Log(LoggerEnum level, string message)
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

        /// <summary>
        /// Method for Information level
        /// </summary>
        /// <param name="value"></param>
        public void Information(string value)
        {
            Log(LoggerEnum.Information, value);
        }

        /// <summary>
        /// Method for Debug level
        /// </summary>
        /// <param name="value"></param>
        public void Debug(string value)
        {
            Log(LoggerEnum.Debug, value);
        }

        /// <summary>
        /// Method for Error level
        /// </summary>
        /// <param name="value"></param>
        public void Error(string value)
        {
            Log(LoggerEnum.Error, value);
        }

        /// <summary>
        /// Method for Fatal level.
        /// </summary>
        /// <param name="value"></param>
        public void Fatal(string value)
        {
            Log(LoggerEnum.Fatal, value);
        }

    }
}
