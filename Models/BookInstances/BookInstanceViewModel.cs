using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models.BookInstances
{
    public class BookInstanceViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }

        public int? Year { get; set; }

        public bool IsAvailable { get; set; }
    }
}
