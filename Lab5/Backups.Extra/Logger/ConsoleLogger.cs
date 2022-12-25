namespace Backups.Extra.Logger;

public class ConsoleLogger : ILogger
{
    public void WriteLog(string log)
    {
        Console.WriteLine(log);
    }
}