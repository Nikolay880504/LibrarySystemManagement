using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models;
using Dapper;
using System.Reflection;
using System.Data.SqlClient;

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
            var addNewBookQuery = @"INSERT INTO Book 
                                 (Title, Author, CategoryId) 
                                 VALUES (@Title, @Author, @CategoryId)";

            _databaseConnection.Connection.Execute(addNewBookQuery, model);
        }




        public IEnumerable<BookListViewModel> GetAllBooks()
        {
            var getAllBookQuery = @"SELECT Book.Id, Title,
                                  Author, 
                                  Category.Name AS Category 
                                  FROM Book 
                                  INNER JOIN 
                                  Category ON Book.CategoryId = Category.Id";          
            
            return _databaseConnection.Connection.Query<BookListViewModel>(getAllBookQuery);
        }

        public void Delete(int id)
        {
            var deleteBookQuery = "DELETE FROM Book WHERE ID = @bookId";

            _databaseConnection.Connection.Execute(deleteBookQuery, new { bookId = id });
        }

        public Book? Get(int id)
        {
            var getBookForIdQuery = @"SELECT Id, 
                                    Title, Author,
                                    CategoryId
                                    FROM Book 
                                    WHERE Id = @bookId";

            return _databaseConnection.Connection.QueryFirstOrDefault<Book>(getBookForIdQuery, new { bookId = id });
        }


        public void Update(Book model)
        {
            var updateBookQuery = @"
                                  UPDATE Book
                                  SET Title = @Title,
                                  Author = @Author,
                                  CategoryID = @CategoryID
                                  WHERE ID = @ID";

            _databaseConnection.Connection.Execute(updateBookQuery, model);
        }



        public BookFormViewModel? GetBookFormViewModel(int id)
        {
            var getBookForIdQuery = @"SELECT Book.Id, 
                                    Title, Author
                                    FROM Book 
                                    INNER JOIN
                                    WHERE Book.Id = @bookId";

            return _databaseConnection.Connection.QueryFirstOrDefault<BookFormViewModel>(getBookForIdQuery, new { bookId = id });
        }
    
    }
}
