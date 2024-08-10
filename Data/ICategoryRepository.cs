using LibrarySystemManagement.Models;

namespace LibrarySystemManagement.Data
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IEnumerable<Category> GetAllCategories();
    }
}
