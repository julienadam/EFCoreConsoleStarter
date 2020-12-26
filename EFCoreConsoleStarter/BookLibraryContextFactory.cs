using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFCoreConsoleStarter
{
    public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T: DbContext
    {
        public T CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DataConnection"));

            return (T) Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }
    }

}