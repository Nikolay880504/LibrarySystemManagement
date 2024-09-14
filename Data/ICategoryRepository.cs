using LibrarySystemManagement.Models.Categories;

namespace LibrarySystemManagement.Data
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IEnumerable<Category> GetAllCategories();
    }
}
