using LibrarySystemManagement.Models.Books;

namespace LibrarySystemManagement.Models.BookInstances
{
    public class BookInstancesForRent
    {
        public int ReaderId { get; set; }   
        public BookViewModel Model { get; set; }
        public List<BookInstanceViewModel> BookInstances { get; set; }
    }
}
