using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Common.Logging;

public static class LoggingService
{
    public static void ConfigureLogging(WebApplicationBuilder builder)
    {
        // Clear existing providers
        builder.Logging.ClearProviders();
        
        // Configure NLog with ASP.NET Core integration
        builder.Host.UseNLog();
        
        // Set log level from configuration
        var logLevel = builder.Configuration.GetValue<string>("Logging:LogLevel:Default") ?? "Information";
        builder.Logging.SetMinimumLevel(GetLogLevel(logLevel));
    }
    
    public static void Shutdown()
    {
        LogManager.Shutdown();
    }
    
    private static Microsoft.Extensions.Logging.LogLevel GetLogLevel(string level) => level.ToLower() switch
    {
        "trace" => Microsoft.Extensions.Logging.LogLevel.Trace,
        "debug" => Microsoft.Extensions.Logging.LogLevel.Debug,
        "information" => Microsoft.Extensions.Logging.LogLevel.Information,
        "warning" => Microsoft.Extensions.Logging.LogLevel.Warning,
        "error" => Microsoft.Extensions.Logging.LogLevel.Error,
        "critical" => Microsoft.Extensions.Logging.LogLevel.Critical,
        _ => Microsoft.Extensions.Logging.LogLevel.Information
    };
}