using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models;
using Dapper;

namespace LibrarySystemManagement.Data
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public SqlBookRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public void Add(Book model)
        {
            var addNewBookQuery = "INSERT INTO Book (Title, Author, Year, Category) VALUES (@Title, @Author, @Year, @Category)";
            _databaseConnection.Connection.Execute(addNewBookQuery, model);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var getAllBookQuery = "SELECT ID, Title, Author, Year, Category FROM Book";
            return _databaseConnection.Connection.Query<Book>(getAllBookQuery);
        }

        public void Delete(int id)
        {
            var deleteBookQuery = "DELETE FROM Book WHERE ID = @bookId";
            _databaseConnection.Connection.Execute(deleteBookQuery, new { bookId = id });
        }

        public Book? Get(int id)
        {
            var getBookForIdQuery = "SELECT ID, Title, Author, Year, Category FROM Book WHERE ID = @bookId";
            return _databaseConnection.Connection.QueryFirstOrDefault<Book>(getBookForIdQuery, new { bookId = id });
        }

        public void Update(Book model)
        {
            var updateBookQuery = @"
            UPDATE Book
            SET Title = @Title,
            Author = @Author,
            Year = @Year,
            Category = @Category
            WHERE ID = @ID";

            _databaseConnection.Connection.Execute(updateBookQuery, model);
        }
    }
}
