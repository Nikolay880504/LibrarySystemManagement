using LibrarySystemManagement.Models.Books;

namespace LibrarySystemManagement.Models.BookInstances
{
    public class BookInstanceCreationModel
    {
        public BookViewModel BookViewModel { get; set; }
        public BookInstance BookInstance { get; set; }
    }
}
