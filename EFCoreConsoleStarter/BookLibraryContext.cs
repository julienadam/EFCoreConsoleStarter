using EFCoreConsoleStarter.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleStarter
{
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Book> Books { get; set; }
    }
}