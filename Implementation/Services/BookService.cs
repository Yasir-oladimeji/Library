using Azure;
using Library.Data;
using Library.Dto;
using Library.Entities;
using Library.Implementation.Repositories;
using Library.Implementation.Services;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Implementation.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;

        public BookService(ApplicationDbContext dbContext, IBookRepository bookRepository, ICategoryService categoryService, ICategoryRepository categoryRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseModel<bool>> CreateBook(CreateBookDto request)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Author = request.Author,
                    Title = request.Title,
                    Pages = request.Pages,
                    YearPublished = request.YearPublished,
                    CategoryId = request.CategoryId,
                    CopiesAvailable = request.CopiesAvailable,
                    Description = request.Description,
                    DateCreated = DateTime.Now
                };
                await _dbContext.Books.AddAsync(book);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Message = "Book Created Successfully ";
                    response.IsSucess = true;
                    response.Data = true;
                    return response;
                }

                response.Message = "Book Creation Failed";
                response.IsSucess = false;
                response.Data = false;
                return response;

            }
            catch (Exception)
            {
                response.Message = "Book Creation Failed ";
                response.IsSucess = false;
            }
            return response;
        }

        public async Task<ResponseModel<bool>> DeleteBook(Guid id)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var book = await _bookRepository.GetBook(id);
                if (book == null)
                {
                    response.IsSucess = false;
                    response.Message = "Book not found";
                    response.Data = false;
                    return response;
                }
                _dbContext.Books.Remove(book);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Message = "Book Deleted Successfully ";
                    response.IsSucess = true;
                    response.Data = true;
                    return response;
                }

                response.Message = "Failed to delete book ";
                response.IsSucess = false;
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = "An error occured";
                response.Data = false;
            }
            return response;
        }

        public async Task<ResponseModel<bool>> EditBook(UpdateBookDto request, Guid id)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var book = await _bookRepository.GetBook(id);
                if (book == null)
                {
                    response.IsSucess = false;
                    response.Message = "Book not found";
                    return response;
                }
                book.Author = request.Author;
                book.Title = request.Title;
                book.Description = request.Description;
                book.Pages = request.Pages;
                book.YearPublished = request.YearPublished;
                book.CopiesAvailable = request.CopiesAvailable;
                book.CategoryId = request.CategoryId;

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Message = "Book Updated Successfully ";
                    response.IsSucess = true;
                    response.Data = true;
                    return response;
                }

                response.Message = "Failed to update book";
                response.IsSucess = false;
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = "An error updating book";
            }
            return response;
        }

        public async Task<ResponseModel<List<BookDto>>> GetAllBooks()
        {
            var response = new ResponseModel<List<BookDto>>();

            try
            {
                var books = await _dbContext.Books
               .Include(x => x.Category)
               .Select(x => new BookDto
               {
                   Id = x.Id,
                   Author = x.Author,
                   Title = x.Title,
                   CategoryId = x.CategoryId,
                   CategoryName = x.Category.Name,
                   Pages = x.Pages,
                   YearPublished = x.YearPublished,
                   CopiesAvailable = x.CopiesAvailable,
                   Description = x.Description
               }).ToListAsync();

                if (books.Count > 0)
                {
                    response.IsSucess = true;
                    response.Message = "Retrieved";
                    response.Data = books;

                    return response;
                }

                response.IsSucess = true;
                response.Message = "No recod found";

                return response;
            }
            catch (Exception ex)
            {

                response.IsSucess = false;
                response.Message = "An error occured";

                return response;
            }
        }

        public async Task<ResponseModel<BookDto>> GetBookDetails(Guid id)
        {
            var response = new ResponseModel<BookDto>();
            try
            {
                var book = await _dbContext.Books
                    .Include(x => x.Category)
                     .Where(x => x.Id == id).FirstOrDefaultAsync();

                if (book == null)
                {
                    response.IsSucess = false;
                    response.Message = "Book not found";
                    return response;
                }
                response.IsSucess = true;
                response.Message = "Retrieved";
                response.Data = new BookDto
                {
                    Id = book.Id,
                    Author = book.Author,
                    Title = book.Title,
                    CategoryId = book.CategoryId,
                    CategoryName = book.Category.Name,
                    Pages = book.Pages,
                    YearPublished = book.YearPublished,
                    CopiesAvailable = book.CopiesAvailable,
                    Description = book.Description
                };

                return response;
            }
            catch (Exception ex)
            {

                response.IsSucess = false;
                response.Message = "An error occured";

                return response;
            }

        }
        public async Task<List<SelectCategory>> SelectCategory()
        {
            var result = new List<SelectCategory>();

            var categorySelect = await _categoryRepository.GetAllCategories();
            if (categorySelect.Count() > 0)
            {
                result = categorySelect.Select(x => new SelectCategory
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

                return result;
            }
            return result;
        }
    }
}

