using LibrarySystemManagement.Models.BookInstances;

namespace LibrarySystemManagement.Models.Books
{
    public class FreeBooksViewModel
    {
        public List<BookInstanceViewModel> Books { get; set; }
        public int ReaderId { get; set; }
    }
}
