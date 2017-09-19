using Microsoft.Extensions.Logging;

public static class LogManager
{
    public static ILoggerFactory LoggerFactory {get; set;}
    public static ILogger GetCurrentClassLogger(string publisherName)
    {
        return LoggerFactory.CreateLogger(publisherName);
    }
}