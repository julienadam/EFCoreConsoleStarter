using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreConsoleStarter
{
    internal class App
    {
        private readonly BookLibraryContext context;

        public App(BookLibraryContext context)
        {
            this.context = context;
        }
        
        public int Run(string[] args)
        {
            // Your code here
            foreach (var book in context.Books)
            {
                Console.WriteLine($"{book.Isbn} : {book.Title}");
            }

            return 1;
        }
    }
}
