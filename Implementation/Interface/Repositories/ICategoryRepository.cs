using Library.Entities;

namespace Library.Implementation.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategory(Guid id);
        Task<Category?> GetCategoryByName(string name);
        Task<List<Category>> GetAllCategories();
    }
}
