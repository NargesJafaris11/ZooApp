namespace ZooApp.Services;

public class ConsoleLoggerService : ILoggerService
{
    public void Log(string message)
    {
        Console.WriteLine($"[ZooApp] {DateTime.Now}: {message}");
    }
}