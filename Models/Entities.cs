using System.Collections.Generic;

namespace automapper_flattened_datasource
{
    public class Library
    {
        public int Id { get; set; }

        public List<LibrarySection> LibrarySections { get; set; }

        public List<LibraryBook> LibraryBooks { get; set; }
    }

    public class LibrarySection
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class LibraryBook
    {
        public int Id { get; set; }

        public Library Library { get; set; }

        public Book Book { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }

        public List<LibraryBook> LibraryBooks { get; set; }

        public string Title { get; set; }
    }
}
