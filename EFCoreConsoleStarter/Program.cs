using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace EFCoreConsoleStarter
{
    internal class Program
    {
        private static void AddCustomDependencies(IServiceCollection services)
        {
            // Add any custom dependencies here
        }
        
        private static int Main(string[] args)
        {
            try
            {
                return 
                    ConfigureServices()
                        .BuildServiceProvider()
                        .GetService<App>()
                        .Run(args);
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
        
        private static ServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();
            
            Log.Information("Logging system initialized");
            
            // Add logging to IoC
            serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder
                    .AddSerilog(dispose: true);
            }));

            serviceCollection.AddLogging();

            // Initialize configuration system using json files like ASP.NET Core
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Install configuration as a singleton in IoC
            serviceCollection.AddSingleton(configuration);
            Log.Information("Configuration loaded and available");

            // Configure EntityFramework
            serviceCollection
                .AddDbContext<BookLibraryContext>(options =>
                    options
                        .UseSqlServer(configuration.GetConnectionString("DataConnection")));

            // Create app
            serviceCollection.AddTransient<App>();
            Log.Information("Application instance create");

            AddCustomDependencies(serviceCollection);

            return serviceCollection;
        }
    }
}
