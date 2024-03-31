
using System.Data.SqlClient;


namespace LibrarySystemManagement.Data.Initializer
{
    public class SqlDatabaseInitializer
    {
        private readonly string _connectionString;

        public SqlDatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }
        private void CreateDatabase(SqlConnection connection)
        {
            using var command = new SqlCommand(GetCreateDatabaseQuery(), connection);
            command.ExecuteNonQuery();

        }

        private void CreateTables(SqlConnection connection)
        {
            using var command = new SqlCommand(GetTabelesQuery(), connection);
            command.ExecuteNonQuery();

        }

        public void InitializeDatabase()
        {
            using var connection = new SqlConnection(GetConnectionStringForCreateDatabase());

            connection.Open();
            if (connection != null && !DatabaseExists(connection))
            {
                CreateDatabase(connection);

                using var useDatabaseCommand = new SqlCommand(GetUseDatabaseQuery(), connection);
                useDatabaseCommand.ExecuteNonQuery();

                CreateTables(connection);
            }
        }

        private string GetUseDatabaseQuery()
        {
            return $"USE {GetDatabaseName()}";
        }

        private bool DatabaseExists(SqlConnection sqlConnection)
        {
            var checkDatabaseQuery = $"SELECT DATABASE_ID FROM sys.databases WHERE Name = '{GetDatabaseName()}'";
            using var command = new SqlCommand(checkDatabaseQuery, sqlConnection);
            var databaseExists = command.ExecuteScalar();
            return (databaseExists != null && databaseExists != DBNull.Value);
        }
        private string GetTabelesQuery()
        {
            return @"CREATE TABLE Book (
                                    ID INT IDENTITY(1,1) PRIMARY KEY,
                                    Title NVARCHAR(255),
                                    Author NVARCHAR(255),
                                    Year INT,
                                    Category NVARCHAR(255)
                                    );

                                    CREATE TABLE Reader (
                                    ID INT IDENTITY(1,1) PRIMARY KEY,
                                    Name NVARCHAR(255),
                                    Email NVARCHAR(255),
                                    RegistrationDate DATETIME
                                    );

                                    CREATE TABLE Borrowing (
                                    ID INT IDENTITY(1,1) PRIMARY KEY,
                                    BookID INT,
                                    ReaderID INT,
                                    BorrowDate DATETIME,
                                    ReturnDate DATETIME,
                                    FOREIGN KEY (BookID) REFERENCES Book(ID) ON DELETE CASCADE,
                                    FOREIGN KEY (ReaderID) REFERENCES Reader(ID) ON DELETE CASCADE
                                    );
                                    ";
        }

        private string GetCreateDatabaseQuery()
        {
            return $"CREATE DATABASE {GetDatabaseName()}";
        }

        private string GetDatabaseName()
        {
            return new SqlConnectionStringBuilder(_connectionString).InitialCatalog;
        }
        private string GetConnectionStringForCreateDatabase()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            builder.InitialCatalog = "";

            string modifiedConnectionString = builder.ToString();
            return modifiedConnectionString;
        }

    }
}
