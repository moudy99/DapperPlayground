using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dapper_VS_EFcore.Configurations
{
    public static class LoggingConfiguration
    {
        public static IHostBuilder SeriLogConfigure(this IHostBuilder hostBuilder)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() // Minimum log level
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}" 
                )
                .WriteTo.File(
                    path: "logs/log-.txt", // Logs folder with daily rolling files
                    rollingInterval: RollingInterval.Day, // New file each day
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}" 
                )
                .CreateLogger();

            // Use Serilog as the logging provider
            return hostBuilder.UseSerilog();
        }
    }
}