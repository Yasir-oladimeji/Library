using Library.Data;
using Library.Dto;
using Library.Entities;
using Library.Implementation.Repositories;
using Library.Implementation.Services;
using Library.Implementation.Repository;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Implementation.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ICategoryRepository categoryRepository, ApplicationDbContext dbContext)
        {
            _categoryRepository = categoryRepository;
            _dbContext = dbContext;
        }

        public async Task<ResponseModel<bool>> CreateCategory(CreateCategoryDto request)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var exitCategory = await _categoryRepository.GetCategoryByName(request.Name);
                if (exitCategory != null)
                {
                    response.Message = $"Category already exist";
                    response.IsSucess = false;
                    response.Data = false;
                    return response;
                }
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    DateCreated = DateTime.Now

                };
                await _dbContext.Categories.AddAsync(category);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Message = "Category Created Successfully ";
                    response.IsSucess = true;
                    response.Data = true;
                    return response;
                }
                response.Message = "Failed to craete category";
                response.IsSucess = false;
                response.Data = false;
                return response;
            }
            catch (Exception)
            {

                response.Message = "Category Creation Failed ";
                response.IsSucess = false;
            }
            return response;
        }

        public async Task<ResponseModel<bool>> DeleteCategory(Guid id)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var category = await _categoryRepository.GetCategory(id);
                if (category == null)
                {
                    response.IsSucess = false;
                    response.Message = "Category not found";
                    response.Data = false;
                    return response;
                }
                _dbContext.Categories.Remove(category);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Message = "Category Deleted Successfully ";
                    response.IsSucess = true;
                    response.Data = true;
                    return response;
                }

                response.Message = "Failed to delete Category ";
                response.IsSucess = false;
                response.Data = false;
                return response;

            }
            catch (Exception)
            {
                response.IsSucess = false;
                response.Message = "An error occured";
                response.Data = false;
            }
            return response;
        }

        public async Task<ResponseModel<bool>> EditCategory(UpdateCategoryDto request, Guid id)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var category = await _categoryRepository.GetCategory(id);
                if (category == null)
                {
                    response.IsSucess = false;
                    response.Message = "Category not found";
                    return response;
                }
                category.Name = request.Name;

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Message = "Category Updated Successfully ";
                    response.IsSucess = true;
                    response.Data = true;
                    return response;
                }

                response.Message = "Failed to update category";
                response.IsSucess = false;
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {

                response.IsSucess = false;
                response.Message = "An error updating category";
            }
            return response;
        }

        public async Task<ResponseModel<List<CategoryDto>>> GetAllCategory()
        {
            var response = new ResponseModel<List<CategoryDto>>();
            try
            {
                var Categories = await _dbContext.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();

                if (Categories.Count > 0)
                {
                    response.IsSucess = true;
                    response.Message = "Retrieved";
                    response.Data = Categories;
                    return response;
                }

                response.IsSucess = true;
                response.Message = "No recod found";
                response.Data = response.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = "An error occured";
            }
            return response;
        }

        public async Task<ResponseModel<CategoryDto>> GetCategoryDetails(Guid id)
        {
            var response = new ResponseModel<CategoryDto>();
            try
            {
                var category = await _dbContext.Categories
          .Where(x => x.Id == id).FirstOrDefaultAsync();

                if (category == null)
                {
                    response.IsSucess = false;
                    response.Message = "Category not found";
                    return response;
                }
                response.IsSucess = true;
                response.Message = "Retrieved";
                response.Data = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,

                };
                return response;

            }
            catch (Exception)
            {
                response.IsSucess = false;
                response.Message = "An error occured";
                return response;
            }
        }
    }
}
