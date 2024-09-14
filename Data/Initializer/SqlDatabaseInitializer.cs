
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

        private void CreateDatabase(SqlConnection connection)
        {
            using var command = new SqlCommand(GetCreateDatabaseQuery(), connection);
            command.ExecuteNonQuery();
        }

        private void CreateTables(SqlConnection connection)
        {
            using var command = new SqlCommand(GetTablesQuery(), connection);
            command.ExecuteNonQuery();
        }

        private bool DatabaseExists(SqlConnection sqlConnection)
        {
            var checkDatabaseQuery = $"SELECT DATABASE_ID FROM sys.databases WHERE Name = '{GetDatabaseName()}'";
            using var command = new SqlCommand(checkDatabaseQuery, sqlConnection);
            var databaseExists = command.ExecuteScalar();
            return (databaseExists != null && databaseExists != DBNull.Value);
        }

        private string GetUseDatabaseQuery()
        {
            return $"USE {GetDatabaseName()}";
        }

        private string GetCreateDatabaseQuery()
        {
            return $"CREATE DATABASE {GetDatabaseName()}";
        }

        private string GetTablesQuery()
        {
            return @"
                CREATE TABLE Category (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(50) NOT NULL
                );

                CREATE TABLE Book (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Title NVARCHAR(255),
                    Author NVARCHAR(255),
                    CategoryId INT,
                    FOREIGN KEY (CategoryId) REFERENCES Category(Id)
                );

                CREATE TABLE BookInstance (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    BookId INT,
                    SerialNumber NVARCHAR(15),
                    Year INT,
                    FOREIGN KEY (BookId) REFERENCES Book(Id) ON DELETE CASCADE
                );

                CREATE TABLE Reader (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(255),
                    Email NVARCHAR(255),
                    RegistrationDate DATETIME
                );

                CREATE TABLE Borrowing (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    BookInstanceId INT,
                    ReaderId INT,
                    BorrowDate DATETIME,
                    ReturnDate DATETIME,
                    ActualReturnDate DATETIME,
                    FOREIGN KEY (BookInstanceId) REFERENCES BookInstance(Id) ON DELETE CASCADE,
                    FOREIGN KEY (ReaderId) REFERENCES Reader(Id)
                );
            ";
        }

        private string GetDatabaseName()
        {
            return new SqlConnectionStringBuilder(_connectionString).InitialCatalog;
        }

        private string GetConnectionStringForCreateDatabase()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString)
            {
                InitialCatalog = ""
            };
            return builder.ToString();
        }
    }
}
