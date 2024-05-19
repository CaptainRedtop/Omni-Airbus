namespace Omni_Airbus.Utils.Logging;

/// <summary>
/// Logger class for writing to a log file.
/// </summary>
public class Logger
{
	private LoggerEnum minLogLevel;
	private string logFilePath = Directory.GetCurrentDirectory() + "/log.txt";
	private string reportFilePath = Directory.GetCurrentDirectory() + "/report.txt";
	private static Queue<KeyValuePair<LoggerEnum, string>> ToBeLogged = new Queue<KeyValuePair<LoggerEnum, string>>();

	/// <summary>
	/// Logger constructor
	/// </summary>
	/// <param name="minLogLevel">The minimum log level that will be logged.</param>
	internal Logger(LoggerEnum minLogLevel)
	{
		this.minLogLevel = minLogLevel;
	}

	/// <summary>
	/// Method for writing to file based on the log level.
	/// </summary>
	public void LogThread()
	{
		while (true)
		{
			if (ToBeLogged.Count > 0)
			{
				KeyValuePair<LoggerEnum, string> log = ToBeLogged.Dequeue();
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
	/// Logs a message with a given level.
	/// </summary>
	/// <param name="level">The log level of the message.</param>
	/// <param name="message">The message to log.</param>
	private void Log(LoggerEnum level, string message)
	{
		if (!string.IsNullOrEmpty(message))
		{
			ToBeLogged.Enqueue(KeyValuePair.Create(level, message));
		}
	}

	/// <summary>
	/// Logs a message at the Report level.
	/// </summary>
	/// <param name="value">The message to log.</param>
	public void Report(string value)
	{
		Log(LoggerEnum.Report, value);
	}

	/// <summary>
	/// Logs a message at the Information level.
	/// </summary>
	/// <param name="value">The message to log.</param>
	public void Information(string value)
	{
		Log(LoggerEnum.Information, value);
	}

	/// <summary>
	/// Logs a message at the Debug level.
	/// </summary>
	/// <param name="value">The message to log.</param>
	public void Debug(string value)
	{
		Log(LoggerEnum.Debug, value);
	}

	/// <summary>
	/// Logs a message at the Error level.
	/// </summary>
	/// <param name="value">The message to log.</param>
	public void Error(string value)
	{
		Log(LoggerEnum.Error, value);
	}

	/// <summary>
	/// Logs a message at the Fatal level.
	/// </summary>
	/// <param name="value">The message to log.</param>
	public void Fatal(string value)
	{
		Log(LoggerEnum.Fatal, value);
	}
}
