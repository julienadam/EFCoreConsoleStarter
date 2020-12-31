using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFCoreConsoleStarter.Entities;

namespace EFCoreConsoleStarter
{
    internal class App
    {
        private readonly IDbContextFactory<BookLibraryContext> contextFactory;

        public App(IDbContextFactory<BookLibraryContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public int Run(string[] args)
        {
            // Your code goes here, check out the samples below
            
            
            //// Adding an entity
            //using (var context = contextFactory.CreateDbContext())
            //{
            //    // Adding a book
            //    context.Add(new Book
            //    {
            //        Authors = "J.R.R. Tolkien",
            //        Isbn = "9780261102385",
            //        Title = "The Lord of the Rings",
            //        PublicationDate = new DateTime(1954, 01, 01),
            //        Summary = "A hobbit and his allies go to Mordor to throw a magic ring in a volcano."
            //    });
            //    context.SaveChanges();
            //}

            //// Modifying an entity
            //using (var context = contextFactory.CreateDbContext())
            //{
            //    var lotr = context.Books.First(b => b.Isbn == "9780261102385");
            //    lotr.Summary = "Sauron, an evil overlord, tries to conquer the world but a band of unlikely heroes gathers to thwart his plans.";
            //    context.SaveChanges();
            //}

            //using (var context = contextFactory.CreateDbContext())
            //{
            //    foreach (var book in context.Books)
            //    {
            //        Console.WriteLine($"{book.Isbn} : {book.Title}");
            //        Console.WriteLine($"{book.Summary}");

            //        if (book.Isbn == "9780261102385")
            //        {
            //            // Deleting an entity
            //            context.Remove(book);
            //        }
            //    }
                
            //    context.SaveChanges();
            //}

            return 1;
        }
    }
}
