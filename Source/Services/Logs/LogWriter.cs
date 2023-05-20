namespace CutieBot.Source.Services.Logs;

public class LogWriter : ILogWriter
{
    private readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/Logs.log");

    public void ClearLogs()
    {
        // This basically writes an empty string in the file
        // overriding it, essentially clearing it.
        File.WriteAllText(LogPath, string.Empty);

        WriteLog("Cleared Logs", LogLevel.Info);
    }

    public void WriteLog(string text, LogLevel level)
    {
        using StreamWriter sw = new StreamWriter(LogPath, true);

        DateTime date = DateTime.Now;
        // Didn't know I could do this, lol.
        string severityString = level.ToString().ToUpper();

        sw.WriteLine($"{date}: [{severityString}] \"{text}\"");
    }
}
