namespace LibrarySystemManagement.Models
{
    public class BorrowingBook
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int ReaderID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Book Book { get; set; }

        public Reader Reader { get; set; }

    }
}
