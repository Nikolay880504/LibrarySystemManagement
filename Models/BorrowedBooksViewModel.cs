namespace LibrarySystemManagement.Models
{
    public class BorrowedBooksViewModel
    {
        public int ReaderId { get; set; }
        public List<BorrowedBookDetailsViewModel> BorrowedBooks { get; set; }
    }
}
