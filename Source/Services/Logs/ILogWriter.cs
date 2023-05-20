using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutieBot.Source.Services.Logs;

public interface ILogWriter
{
    void WriteLog(string text, LogLevel level);
    void ClearLogs();
}
