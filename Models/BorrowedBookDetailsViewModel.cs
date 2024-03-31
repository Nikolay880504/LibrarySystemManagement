

namespace LibrarySystemManagement.Models
{
    public class BorrowedBookDetailsViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }    
        public string Author { get; set; }   
        public string Category { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
