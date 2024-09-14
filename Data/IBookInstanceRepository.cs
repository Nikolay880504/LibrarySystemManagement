using LibrarySystemManagement.Models.BookInstances;

namespace LibrarySystemManagement.Data
{
    public interface IBookInstanceRepository : IBaseRepository<BookInstance>
    {
        List<BookInstanceViewModel> GetAllBookInstancesForBookId(int id);
    }
}
