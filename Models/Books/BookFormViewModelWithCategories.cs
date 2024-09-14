using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibrarySystemManagement.Models.Books
{
    public class BookFormViewModelWithCategories
    {
        public BookFormViewModel Form { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
