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

            var connectionString = configuration.GetConnectionString("DataConnection");
            serviceCollection.AddContextFactory<BookLibraryContext>(connectionString);
            
            // Create app
            serviceCollection.AddTransient<App>();
            Log.Information("Application instance create");

            AddCustomDependencies(serviceCollection);

            return serviceCollection;
        }
    }

    public static class DbContextFactoryExtensions
    {
        public static void AddContextFactory<T>(this IServiceCollection serviceCollection, string connectionString) where T : DbContext
        {
            // Add the context itself, as transient
            serviceCollection
                .AddDbContext<T>(options =>
                        options.UseSqlServer(connectionString), ServiceLifetime.Transient);
            
            // Add the factory that will create an instance on demand
            serviceCollection.AddSingleton<IDbContextFactory<T>, DbContextFactory<T>>();
        }
    }

    public interface IDbContextFactory<out T> where T : DbContext
    {
        T CreateDbContext();
    }

    public class DbContextFactory<T> : IDbContextFactory<T> where T: DbContext
    {
        private readonly IServiceProvider services;

        public DbContextFactory(IServiceProvider services)
        {
            this.services = services;
        }

        public T CreateDbContext()
        {
            return services.GetService<T>();
        }
    }
}
