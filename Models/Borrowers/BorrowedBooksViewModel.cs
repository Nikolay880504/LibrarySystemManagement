using LibrarySystemManagement.Models.Readers;

namespace LibrarySystemManagement.Models.Borrowers
{
    public class BorrowedBooksViewModel
    {
        public Reader Reader { get; set; }
        public List<BorrowedBookDetailsViewModel> BorrowedBooks { get; set; }
    }
}
