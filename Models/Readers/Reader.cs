using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models.Readers
{
    public class Reader
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Registration date is requied.")]
        public DateTime RegistrationDate { get; set; }
    }
}
