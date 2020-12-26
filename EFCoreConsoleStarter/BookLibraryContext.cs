using EFCoreConsoleStarter.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleStarter
{
    // This class is used by the dotnet ef tools to create db contexts when the program is not running
    public class BookLibraryDesignTimeDbContextFactory : DesignTimeDbContextFactory<BookLibraryContext> { }
    
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}