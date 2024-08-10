namespace LibrarySystemManagement.Models
{
    public class BorrowedBooksViewModel
    {
        public Reader Reader { get; set; }
        public List<BorrowedBookDetailsViewModel> BorrowedBooks { get; set; }
    }
}
