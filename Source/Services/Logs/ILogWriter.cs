namespace CutieBot.Source.Services.Logs;

public interface ILogWriter
{
    void WriteLog(string text, LogLevel level);
    void ClearLogs();
}
