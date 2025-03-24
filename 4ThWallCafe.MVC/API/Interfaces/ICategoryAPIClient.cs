using _4ThWallCafe.API.Model;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface ICategoryAPIClient
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task EditCategoryAsync(EditCategory category);
        Task CreateCategoryAsync(AddCategory category);
    }
}
