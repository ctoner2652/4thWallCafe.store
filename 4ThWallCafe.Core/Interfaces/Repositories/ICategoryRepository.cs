using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        List<Category> GetAllCategories();
        void EditCategory(Category category);
        void AddCategory(Category category);
        Category GetCategoryByName(string name);
    }
}
