using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Year is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Year must be a valid number.")]
        public int Year { get; set; }
        [Required(ErrorMessage ="Author is required.")]
        public string Author { get; set; }
        [Required(ErrorMessage ="Category is required.")]
        public string Category { get; set; }

    }
}
