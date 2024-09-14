using LibrarySystemManagement.Models.Books;

namespace LibrarySystemManagement.Models.BookInstances
{
    public class BookWithInstancesListViewModel
    {
        public BookViewModel BookViewModel { get; set; }
        public List<BookInstanceViewModel> BookInstances { get; set; }
    }
}
