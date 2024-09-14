using LibrarySystemManagement.Models.Borrowers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LibrarySystemManagement.Data
{
    public interface IBorrowingRepository : IBaseRepository<BorrowingBook>
    {
      List<BookBorrowedByReader> GetAllBorrowingBooks();
      List<BorrowedBookDetailsViewModel> GetBorrowedBooksByReaderId(int readerId);

    }
}
