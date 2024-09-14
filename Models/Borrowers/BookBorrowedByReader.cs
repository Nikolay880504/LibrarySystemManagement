using LibrarySystemManagement.Models.BookInstances;
using LibrarySystemManagement.Models.Books;
using LibrarySystemManagement.Models.Readers;

namespace LibrarySystemManagement.Models.Borrowers
{
    public class BookBorrowedByReader
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ReturnStatus {get; set;}
    }
}
