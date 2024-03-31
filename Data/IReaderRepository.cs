using LibrarySystemManagement.Models;

namespace LibrarySystemManagement.Data
{
    public interface IReaderRepository : IBaseRepository<Reader>
    {
      IEnumerable<Reader> GetAllReaders();
    }
}
