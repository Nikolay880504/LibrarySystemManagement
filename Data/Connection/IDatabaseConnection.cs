using System.Data;

namespace LibrarySystemManagement.Data.Connection
{
    public interface IDatabaseConnection
    {
        IDbConnection Connection { get; }
    }
}
