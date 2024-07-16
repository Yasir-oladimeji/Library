using AspNetCoreHero.ToastNotification.Abstractions;
using Library.Dto;
using Library.Implementation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _BookService;
        private readonly INotyfService _notyf;
        private readonly ICategoryService _categoryService;

        public BookController(IBookService BookService, INotyfService notyf, ICategoryService categoryService)
        {
            _BookService = BookService;
            _notyf = notyf;
            _categoryService = categoryService;
        }
        [HttpGet("books")]
        public async Task<IActionResult> Books()
        {

            var book = await _BookService.GetAllBooks();
            if (book.IsSucess)
            {
                return View(book.Data);
            }

            return View();
        }

        [HttpGet("book/{id}")]
        public async Task<IActionResult> Book(Guid id)
        {
            var book = await _BookService.GetBookDetails(id);
            if (book.IsSucess)
            {
                return View(book.Data);
            }
            return RedirectToAction(nameof(Books));
        }

        [HttpGet("create-book")]
        public async Task<IActionResult> CreateBook()
        {
            
            var result = await _BookService.SelectCategory();

            ViewData["SelectCategories"] = new SelectList(result.ToList(),"Id", "Name");

            return View();
        }


        [HttpPost("create-book")]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookDto request)
        {
            var result = await _BookService.CreateBook(request);
            if (result.IsSucess)
            {
                _notyf.Success(result.Message);
                return RedirectToAction(nameof(Books));
            }
            _notyf.Error(result.Message);
            return View();
        }

        [HttpGet("book/edit/{id}")]
        public async Task<IActionResult> EditBook(Guid id)
        {
            var response = await _BookService.GetBookDetails(id);
            if (response.IsSucess)
            {
                return View(response.Data);
            }
            _notyf.Error(response.Message);
            return RedirectToAction(nameof(Books));
        }

        [HttpPost("book/edit/{id}")]
        public async Task<IActionResult> EditBook(Guid id, UpdateBookDto request)
        {
            var result = await _BookService.EditBook(request, id);
            if (result.IsSucess)
            {
                _notyf.Success(result.Message);
                return RedirectToAction(nameof(Books));
            }
            _notyf.Error(result.Message);
            return RedirectToAction("Books");
        }

        [HttpGet("book/delete/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var result = await _BookService.DeleteBook(id);
            if (result.IsSucess)
            {
                _notyf.Error(result.Message);
                return RedirectToAction(nameof(Books));
            }
            _notyf.Error(result.Message);
            return View(result);
        }
    }
}

