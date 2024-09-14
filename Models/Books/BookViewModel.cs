using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Category { get; set; }

    }
}
