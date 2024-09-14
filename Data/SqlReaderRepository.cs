using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models.Readers;

namespace LibrarySystemManagement.Data
{
    public class SqlReaderRepository : IReaderRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public SqlReaderRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Add(Reader model)
        {
            var addNewReaderQuery = @"
                INSERT INTO Reader (Name, Email, RegistrationDate) 
                VALUES (@Name, @Email, @RegistrationDate)";

            _databaseConnection.Connection.Execute(addNewReaderQuery, model);
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            var getAllReadersQuery = @"
                SELECT 
                    ID, 
                    Name, 
                    Email, 
                    RegistrationDate 
                FROM Reader";

            return _databaseConnection.Connection.Query<Reader>(getAllReadersQuery);
        }

        public void Delete(int id)
        {
            var deleteReaderQuery = @"
                DELETE FROM Reader 
                WHERE ID = @ReaderId";

            _databaseConnection.Connection.Execute(deleteReaderQuery, new { ReaderId = id });
        }

        public Reader? Get(int id)
        {
            var getReaderByIdQuery = @"
                SELECT 
                    ID, 
                    Name, 
                    Email, 
                    RegistrationDate 
                FROM Reader 
                WHERE ID = @ReaderId";

            return _databaseConnection.Connection
                .QueryFirstOrDefault<Reader>(getReaderByIdQuery, new { ReaderId = id });
        }

        public void Update(Reader model)
        {
            var updateReaderQuery = @"
                UPDATE Reader 
                SET 
                    Name = @Name,
                    Email = @Email,
                    RegistrationDate = @RegistrationDate
                WHERE ID = @ID";

            _databaseConnection.Connection.Execute(updateReaderQuery, model);
        }
    }
}

