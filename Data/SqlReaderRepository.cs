using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models;

namespace LibrarySystemManagement.Data
{
    public class SqlReaderRepository : IReaderRepository
    {
        readonly private IDatabaseConnection _databaseConnection;
        public SqlReaderRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection; ;
        }
        public void Add(Reader model)
        {

            var addNewReaderQuery = "INSERT INTO Reader(Name, Email, RegistrationDate) VALUES (@Name, @Email, @RegistrationDate)";
            _databaseConnection.Connection.Execute(addNewReaderQuery, model);
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            var allGetReadersQuery = "SELECT ID, Name, Email, RegistrationDate FROM Reader";
            return _databaseConnection.Connection.Query<Reader>(allGetReadersQuery);
        }

        public void Delete(int id)
        {
            var deleteRaederQuery = "DELETE FROM Reader WHERE ID = @readerId";
            _databaseConnection.Connection.Execute(deleteRaederQuery, new { readerId = id });
        }

        public Reader? Get(int id)
        {
            var getReaderForIdQuery = "SELECT ID, Name, Email, RegistrationDate FROM Reader WHERE ID  = @readerId";
            return _databaseConnection.Connection.QueryFirstOrDefault<Reader>(getReaderForIdQuery, new { readerId = id });
        }

        public void Update(Reader model)
        {
            var updateRederQuery = @"UPDATE Reader 
                                 SET Name = @Name,
                                 Email = @Email,
                                 RegistrationDate = @RegistrationDate
                                 WHERE ID = @ID";
            _databaseConnection.Connection.Execute(updateRederQuery, model);
        }
    }
}
