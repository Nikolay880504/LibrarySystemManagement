using LibrarySystemManagement.Models.Books;

namespace LibrarySystemManagement.Data
{
    public interface IBookRepository : IBaseRepository<Book>
    {   
        BookViewModel GetBookById(int id);

        List<BookViewModel> GetBooksWithCategories();

        List<BookViewModel> GetListMostPopularBooks();

    }
}
