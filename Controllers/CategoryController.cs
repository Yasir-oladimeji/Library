using AspNetCoreHero.ToastNotification.Abstractions;
using Library.Dto;
using Library.Implementation.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly INotyfService _notyf;
        public CategoryController(ICategoryService CategoryService, INotyfService notyf)
        {
            _CategoryService = CategoryService;
            _notyf = notyf;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Categories()
        {
            var category = await _CategoryService.GetAllCategory();
            if (category.IsSucess)
            {
                return View(category.Data);
            }

            return View();
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> Category(Guid id)                      
        {
            var category = await _CategoryService.GetCategoryDetails(id);
            if (category.IsSucess)
            {
                return View(category.Data);
            }
            return RedirectToAction("Categories");
        }

        [HttpGet("create-category")]
        public IActionResult CreateCategory() =>
             View();

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto request)
        {
            var result = await _CategoryService.CreateCategory(request);
            if (result.IsSucess)
            {
                _notyf.Success(result.Message);
                return RedirectToAction(nameof(Categories));
            }
            _notyf.Error(result.Message);
            return RedirectToAction(nameof(CreateCategory));
        }

        [HttpGet("category/edit/{id}")]
        public async Task<IActionResult> EditCategory(Guid id)
        {
            var response = await _CategoryService.GetCategoryDetails(id);
            if (response.IsSucess)
            {
                return View(response.Data);
            }
            _notyf.Error(response.Message);
            return RedirectToAction(nameof(Categories));
        }

        [HttpPost("category/edit/{id}")]
        public async Task<IActionResult> EditCategory(Guid id, UpdateCategoryDto request)
        {
            var result = await _CategoryService.EditCategory(request, id);
            if (result.IsSucess)
            {
                _notyf.Success(result.Message);
                return RedirectToAction(nameof(Categories));
            }
            _notyf.Error(result.Message);
            return RedirectToAction("Categories");
        }

        [HttpGet("category/delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var result = await _CategoryService.DeleteCategory(id);
            if (result.IsSucess)
            {
                _notyf.Error(result.Message);
                return RedirectToAction(nameof(Categories));
            }
            _notyf.Error(result.Message);
            return RedirectToAction(nameof(Categories));
        }
    }
}
