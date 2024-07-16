using Library.Entities;

namespace Library.Implementation.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBook(Guid id);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetAllBooksByCategoryId(Guid CategoryId);
    }
}
