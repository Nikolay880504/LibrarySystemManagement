using LibrarySystemManagement.Models;


namespace LibrarySystemManagement.Data
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        IEnumerable<BookListViewModel> GetAllBooks();     
        public BookFormViewModel GetBookFormViewModel(int id);
    }
}
