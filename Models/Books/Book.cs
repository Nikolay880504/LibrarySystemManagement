using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models.Books
{
    public class Book
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
   
        public string Author { get; set; }
   
        public int? CategoryId { get; set; }

    }
}
