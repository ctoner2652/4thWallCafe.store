using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Result<Category> GetCategory(int id);
        Result<List<Category>> GetAllCategories();
        Result EditCategory(Category category);
        Result AddCategory(Category category);
    }
}
