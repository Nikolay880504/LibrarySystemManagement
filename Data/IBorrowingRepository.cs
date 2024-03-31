using LibrarySystemManagement.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LibrarySystemManagement.Data
{
    public interface IBorrowingRepository : IBaseRepository<BorrowingBook>
    {
      List<BookBorrowedByReader> GetAllBorrowingBooks();
      IEnumerable<BorrowedBookDetailsViewModel> GetAllBorrowingBooksByReaderId(int readerId);

    }
}
