using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models.Categories;

namespace LibrarySystemManagement.Data
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public SqlCategoryRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Add(Category model)
        {
            var addNewCategoryQuery = @"
                INSERT INTO Category (Name) 
                VALUES (@Name)";

            _databaseConnection.Connection.Execute(addNewCategoryQuery, model);
        }

        public void Delete(int id)
        {
            var deleteCategoryQuery = @"
                DELETE FROM Category 
                WHERE Id = @Id";

            _databaseConnection.Connection.Execute(deleteCategoryQuery, new { Id = id });
        }

        public Category? Get(int id)
        {
            var getCategoryByIdQuery = @"
                SELECT * FROM Category 
                WHERE Id = @Id";

            return _databaseConnection.Connection
                .QueryFirstOrDefault<Category>(getCategoryByIdQuery, new { Id = id });
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var getAllCategoriesQuery = @"
                SELECT * FROM Category";

            return _databaseConnection.Connection
                .Query<Category>(getAllCategoriesQuery)
                .ToList();
        }

        public void Update(Category model)
        {
            var updateCategoryQuery = @"
                UPDATE Category 
                SET Name = @Name 
                WHERE Id = @Id";

            _databaseConnection.Connection.Execute(updateCategoryQuery, model);
        }
    }
}
