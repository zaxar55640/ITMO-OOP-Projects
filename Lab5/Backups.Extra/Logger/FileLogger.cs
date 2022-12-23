using Backups.Extra.Exceptions;

namespace Backups.Extra.Logger;

public class FileLogger : ILogger
{
    private string logPath;

    public FileLogger(string path)
    {
        if (path == string.Empty) throw new LoggerException("Wrong path for file logging.");
        logPath = path;
    }

    public void WriteLog(string log)
    {
        StreamWriter message = new StreamWriter(logPath);
        message.WriteLine(DateTime.Now + log);
    }
}