using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.Extensions.Logging;

namespace _4ThWallCafe.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ILogger _logger;
        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public Result AddCategory(Category category)
        {
            try
            {
                var duplicate = _categoryRepository.GetCategoryByName(category.CategoryName);

                if (duplicate != null)
                {
                    return ResultFactory.Fail($"Category with name : {category.CategoryName} already exist!");
                }

                _categoryRepository.AddCategory(category);
                return ResultFactory.Success();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditCategory(Category category)
        {
            try
            {
                var duplicate = _categoryRepository.GetCategoryByName(category.CategoryName);

                if (duplicate != null && duplicate.CategoryId != category.CategoryId)
                {
                    return ResultFactory.Fail($"Category with name : {category.CategoryName} already exist!");
                }

                _categoryRepository.EditCategory(category);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Category>> GetAllCategories()
        {
            try
            {
                var categories = _categoryRepository.GetAllCategories();
                return ResultFactory.Success(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<Category>>(ex.Message);
            }
        }

        public Result<Category> GetCategory(int id)
        {
            try
            {
                var category = _categoryRepository.GetCategory(id);
                return category is null ? ResultFactory.Fail<Category>($"Category with ID : {id} not found. ") :
                    ResultFactory.Success(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<Category>(ex.Message);
            }
        }
    }
}
