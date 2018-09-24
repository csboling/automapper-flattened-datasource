using System.Collections.Generic;

namespace automapper_flattened_datasource
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
    }

    public class LibraryDto : BaseDto
    {
        public List<SectionDto> Sections { get; set; }

        public List<BookDto> Books { get; set; }
    }

    public class SectionDto : BaseDto
    {
        public string Name { get; set; }
    }

    public class BookDto : BaseDto
    {
        public string Title { get; set; }
    }    
}
