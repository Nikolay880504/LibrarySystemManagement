using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models;

namespace LibrarySystemManagement.Data
{
    public class SqlCaregoryRepository : ICategoryRepository
    {
        readonly private IDatabaseConnection _databaseConnection;

        public SqlCaregoryRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public void Add(Category model)
        {
            var addNewCatogoryQuerry = $"INSERT INTO Category (name) VALUES (@Name)";
            _databaseConnection.Connection.Execute(addNewCatogoryQuerry, model);
        }

        public void Delete(int id)
        {
            var deleteCategoryQuery = $"DELETE FROM Category WHERE Id = @id";
            _databaseConnection.Connection.Execute(deleteCategoryQuery, new {Id = id});
        }

        public Category? Get(int id)
        {
            var getCategoryForIdQery = $"SELECT * FROM Category WHERE Id = @id";
            
            return _databaseConnection.Connection.QueryFirstOrDefault<Category>(getCategoryForIdQery, new {Id = id});
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var getAllCategoriesQuery = "SELECT * FROM Category";

            return _databaseConnection.Connection.Query<Category>(getAllCategoriesQuery).ToList();
        }

        public void Update(Category model)
        {
            var updateCategoryQuery = $"UPDATE  Category SET Name = @Name WHERE Id = @Id";
            _databaseConnection.Connection.Execute(updateCategoryQuery, model);

        }
    }
}
