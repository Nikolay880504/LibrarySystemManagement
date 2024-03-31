namespace LibrarySystemManagement.Models
{
    public class BookBorrowedByReader
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
