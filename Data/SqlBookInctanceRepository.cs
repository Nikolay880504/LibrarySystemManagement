using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models;
using System.ComponentModel.DataAnnotations;

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
            var insertQuery = @"INSERT INTO BookInstance (BookId, SerialNumber, Year, IsAvailable)
                                VALUES (@BookId, @SerialNumber, @Year, @IsAvailable)";
                                

            _databaseConnection.Connection.Execute(insertQuery, model);
        }
        void IBaseRepository<BookInstance>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        BookInstance IBaseRepository<BookInstance>.Get(int id)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<BookInstance>.Update(BookInstance model)
        {
            var updateBookInstanceQuery = @"UPDATE BookInstance 
                                          SET
                                          Quantity = @Quantity
                                          CurrentQuantity = @CurrentQuantity
                                          WHERE Id = @Id";
            _databaseConnection.Connection.Execute(updateBookInstanceQuery, model);
        }
    }
}
