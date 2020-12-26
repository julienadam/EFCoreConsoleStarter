using System;
using System.Collections.ObjectModel;

namespace EFCoreConsoleStarter.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Isbn { get; set; }
        public string Summary { get; set; }
    }
}