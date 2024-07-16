using Library.Data;
using Library.Entities;
using Library.Implementation.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Implementation.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Book>> GetAllBooks() =>
            await _dbContext.Books.ToListAsync();
        public async Task<Book?> GetBook(Guid id) =>
            await _dbContext.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<Book>> GetAllBooksByCategoryId(Guid categoryId) =>
            await _dbContext.Books.Where(x => x.CategoryId == categoryId).ToListAsync();
    }
}
