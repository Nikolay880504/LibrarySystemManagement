using LibrarySystemManagement.Models.Books;
using LibrarySystemManagement.Models.Readers;

namespace LibrarySystemManagement.Models.Borrowers
{
    public class BorrowingBook
    {
        public int Id { get; set; }
        public int BookInstanceId { get; set; }
        public int ReaderId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public DateTime ActualReturnDate { get; set; }

        public Book Book { get; set; }

        public Reader Reader { get; set; }

    }
}
