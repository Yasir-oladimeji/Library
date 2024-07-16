using Library.Dto;
using Library.Entities;
using Library.Models;

namespace Library.Implementation.Services
{
    public interface IBookService
    {
        Task<ResponseModel<bool>> CreateBook(CreateBookDto request);
        Task<ResponseModel<bool>> EditBook(UpdateBookDto request, Guid id);
        Task<ResponseModel<bool>> DeleteBook(Guid id);
        Task<ResponseModel<List<BookDto>>> GetAllBooks();
        Task<ResponseModel<BookDto>> GetBookDetails(Guid id);
        Task<List<SelectCategory>> SelectCategory();
    }
}
