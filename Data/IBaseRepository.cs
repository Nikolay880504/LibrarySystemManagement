

namespace LibrarySystemManagement.Data
{
    public interface IBaseRepository<T> 
    {
        
        void Add(T model);
        T Get(int id);
        void Update(T model);
        void Delete(int id);
    }
}
