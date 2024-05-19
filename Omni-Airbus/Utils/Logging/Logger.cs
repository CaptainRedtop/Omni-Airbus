namespace Omni_Airbus.Utils.Logging;

/// <summary>
/// Logger class for writing to a log file.
/// </summary>
public class Logger
{
    private LoggerEnum minLogLevel;
    private string logFilePath = Directory.GetCurrentDirectory() + "/log.txt";
    private string reportFilePath = Directory.GetCurrentDirectory() + "/report.txt";
    private static Queue<KeyValuePair<LoggerEnum, string>> ToBeLongged = new Queue<KeyValuePair<LoggerEnum, string>>();

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
    public void LogThread()
    {
        while (true)
        {
            if (ToBeLongged.Count > 0)
            {
                KeyValuePair<LoggerEnum, string> log = ToBeLongged.Dequeue();
                LoggerEnum level = log.Key;
                string message = log.Value;

                if (level >= minLogLevel && !string.IsNullOrEmpty(message))
                {
                    string logEntry = $"[{DateTime.Now}] [{level}] {message}";
                    if (level != LoggerEnum.Report)
                    {
                        using (StreamWriter writer = new StreamWriter(logFilePath, true))
                        {
                            writer.WriteLine(logEntry);
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = new StreamWriter(reportFilePath, true))
                        {
                            writer.WriteLine(logEntry);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    private void Log(LoggerEnum level, string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            ToBeLongged.Enqueue(KeyValuePair.Create(level, message));
        }
    }

    /// <summary>
    /// Method for Report level
    /// </summary>
    /// <param name="value"></param>
    public void Report(string value)
    {
        Log(LoggerEnum.Report, value);
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
