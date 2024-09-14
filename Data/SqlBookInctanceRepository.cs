using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models.BookInstances;

namespace LibrarySystemManagement.Data
{
    public class SqlBookInctanceRepository : IBookInstanceRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public SqlBookInctanceRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Add(BookInstance model)
        {
            var insertQuery = @"
                INSERT INTO BookInstance 
                (BookId, SerialNumber, Year)
                VALUES (@BookId, @SerialNumber, @Year)";

            _databaseConnection.Connection.Execute(insertQuery, model);
        }

        public List<BookInstanceViewModel> GetAllBookInstancesForBookId(int id)
        {
            var getAllBooksInstancesQuery = @"
                               SELECT 
                                 BookInstance.Id,
                                 BookInstance.Year, 
                                 BookInstance.SerialNumber,
                                   CASE 
                                    WHEN EXISTS (
                                    SELECT 1 
                                    FROM Borrowing
                                    WHERE BookInstance.Id = Borrowing.BookInstanceId 
                                    AND ActualReturnDate IS NULL
                                    )
                                    THEN CAST(0 AS BIT)  
                                    ELSE CAST(1 AS BIT)
                               END AS IsAvailable 
                               FROM 
                               BookInstance
                               WHERE 
                               BookInstance.BookId = @Id;";

            return _databaseConnection.Connection
                .Query<BookInstanceViewModel>(getAllBooksInstancesQuery, new { Id = id })
                .ToList();
        }
        public void Delete(int id)
        {
            var deleteBookInstanceQuery = @"
                DELETE FROM BookInstance 
                WHERE Id = @Id;";

            _databaseConnection.Connection.Execute(deleteBookInstanceQuery, new { Id = id });
        }

        public BookInstance? Get(int id)
        {
            var getBookInstanceByIdQuery = @"
                SELECT * 
                FROM BookInstance 
                WHERE Id = @Id;";

            return _databaseConnection.Connection
                .QuerySingleOrDefault<BookInstance>(getBookInstanceByIdQuery, new { Id = id });
        }

        public void Update(BookInstance model)
        {
            var updateBookInstanceQuery = @"
                UPDATE BookInstance 
                SET
                    SerialNumber = @SerialNumber,
                    Year = @Year
                WHERE Id = @Id";

            _databaseConnection.Connection.Execute(updateBookInstanceQuery, model);
        }
    }
}
