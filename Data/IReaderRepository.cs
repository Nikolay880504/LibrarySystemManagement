using LibrarySystemManagement.Models.Readers;

namespace LibrarySystemManagement.Data
{
    public interface IReaderRepository : IBaseRepository<Reader>
    {
      IEnumerable<Reader> GetAllReaders();
    }
}
