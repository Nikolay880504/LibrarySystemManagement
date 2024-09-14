using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models.BookInstances
{
    public class BookInstance
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [Required(ErrorMessage = "Serial number is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Year must be a valid number.")]
        public int? Year { get; set; }
    }
}
