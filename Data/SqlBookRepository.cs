using LibrarySystemManagement.Data.Connection;
using Dapper;
using LibrarySystemManagement.Models.Books;

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
            var addNewBookQuery = @"
                INSERT INTO Book 
                (Title, Author, CategoryId) 
                VALUES (@Title, @Author, @CategoryId)";

            _databaseConnection.Connection.Execute(addNewBookQuery, model);
        }

        public List<BookViewModel> GetBooksWithCategories()
        {
            var getAllBookQuery = @"
                SELECT 
                    Book.Id AS Id, 
                    Title,
                    Author, 
                    Category.Name AS Category 
                FROM Book 
                INNER JOIN Category 
                    ON Book.CategoryId = Category.Id";

            return _databaseConnection.Connection
                .Query<BookViewModel>(getAllBookQuery)
                .ToList();
        }
        public List<BookViewModel> GetListMostPopularBooks()
        {
            var getPopularBooksQuery = @"
                SELECT 
                   Book.ID AS Id,
                   Title,
                   Author,
                   Category.Name AS Category,
                   COUNT(BookInstance.BookId) AS InstanceCount
                FROM Book
                INNER JOIN 
                Category
                   ON Book.CategoryId = Category.Id
                   
                INNER JOIN                  
                BookInstance 
                 ON BookInstance.BookId = Book.Id
                
                INNER JOIN 
                Borrowing  
                   ON BookInstance.Id  = Borrowing.BookInstanceId
                   GROUP BY Book.ID,
                   Book.Title,
                   Book.Author,
                   Category.Name
                   HAVING COUNT( BookInstance.BookId ) > 1
                   ORDER BY InstanceCount DESC;";
                

            return _databaseConnection.Connection
              .Query<BookViewModel>(getPopularBooksQuery)
              .ToList();

        } 
        public void Delete(int id)
        {
            var deleteBookQuery = @"
                DELETE FROM Book 
                WHERE ID = @bookId";

            _databaseConnection.Connection.Execute(deleteBookQuery, new { bookId = id });
        }

        public Book? Get(int id)
        {
            var getBookForIdQuery = @"
                SELECT 
                    Id, 
                    Title, 
                    Author,
                    CategoryId
                FROM Book 
                WHERE Id = @bookId";

            return _databaseConnection.Connection
                .QueryFirstOrDefault<Book>(getBookForIdQuery, new { bookId = id });
        }

        public void Update(Book model)
        {
            var updateBookQuery = @"
                UPDATE Book
                SET 
                    Title = @Title,
                    Author = @Author,
                    CategoryId = @CategoryId
                WHERE Id = @Id";

            _databaseConnection.Connection.Execute(updateBookQuery, model);
        }

        public BookViewModel? GetBookById(int id)
        {
            var getBookForIdQuery = @"
                SELECT 
                    Book.Id, 
                    Title, 
                    Author,
                    Category.Name AS Category
                FROM Book 
                INNER JOIN Category 
                    ON Category.Id = Book.CategoryId
                WHERE Book.Id = @bookId";

            return _databaseConnection.Connection
                .QueryFirstOrDefault<BookViewModel>(getBookForIdQuery, new { bookId = id });
        }
    }
}
