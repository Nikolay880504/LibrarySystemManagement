namespace LibrarySystemManagement.Models.Borrowers
{
    public class BorrowedBookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string SerialNumber { get; set; } 
        public string Author { get; set; }
        public string Category { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
