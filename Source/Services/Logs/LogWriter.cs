using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutieBot.Source.Services.Logs;

public class LogWriter : ILogWriter
{
    private readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/Logs.log");

    public void ClearLogs()
    {
        File.WriteAllText(LogPath, string.Empty);

        WriteLog("Cleared Logs", LogLevel.Info);
    }

    public void WriteLog(string text, LogLevel level)
    {
        using StreamWriter sw = new StreamWriter(LogPath, true);

        DateTime date = DateTime.Now;
        string severityString = level.ToString().ToUpper();

        sw.WriteLine($"{date}: [{severityString}] \"{text}\"");
    }
}
