using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EFCoreConsoleStarter
{
    internal class Program
    {
        private static int ConfiguredMain(IConfigurationRoot configuration, string[] args)
        {
            // Votre code ici
            return 1;
        }

        private static int Main(string[] args)
        {
            // Initialize Serilog logger
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                 .MinimumLevel.Debug()
                 .Enrich.FromLogContext()
                 .CreateLogger();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            try
            {
                return ConfiguredMain(configuration, args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unhandled exception in Main.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
