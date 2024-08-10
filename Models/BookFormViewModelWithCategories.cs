using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibrarySystemManagement.Models
{
    public class BookFormViewModelWithCategories
    {
        public BookFormViewModel Form { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
