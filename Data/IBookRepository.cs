using LibrarySystemManagement.Models;


namespace LibrarySystemManagement.Data
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        IEnumerable<Book> GetAllBooks();
    }
}
