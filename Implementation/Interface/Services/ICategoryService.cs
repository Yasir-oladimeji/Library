using Library.Dto;
using Library.Entities;
using Library.Models;

namespace Library.Implementation.Services
{

    public interface ICategoryService
    {
        Task<ResponseModel<bool>> CreateCategory(CreateCategoryDto request);
        Task<ResponseModel<bool>> EditCategory(UpdateCategoryDto request, Guid id);
        Task<ResponseModel<bool>> DeleteCategory(Guid id);
        Task<ResponseModel<List<CategoryDto>>> GetAllCategory();
        Task<ResponseModel<CategoryDto>> GetCategoryDetails(Guid id);
    }
}
