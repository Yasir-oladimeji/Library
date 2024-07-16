using Library.Data;
using Library.Entities;
using Library.Implementation.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Implementation.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetAllCategories() => 
            await _dbContext.Categories.ToListAsync();
        public async Task<Category?> GetCategory(Guid id) => 
            await _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<Category?> GetCategoryByName(string name) => 
            await _dbContext.Categories.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
    }
}
