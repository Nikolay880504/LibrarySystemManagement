using System.Data;
using System.Data.SqlClient;

namespace LibrarySystemManagement.Data.Connection
{
    public class SqlDataBaseConnection : IDatabaseConnection, IDisposable
    {
        private readonly IDbConnection _connection;
        public SqlDataBaseConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }
        public IDbConnection Connection => _connection;

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
