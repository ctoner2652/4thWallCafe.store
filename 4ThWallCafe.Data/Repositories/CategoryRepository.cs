using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private FourthWallCafeContext _dbContext;

        public CategoryRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCategory(Category category)
        {
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            _dbContext.Category.Update(category);
            _dbContext.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return _dbContext.Category.ToList();
        }

        public Category GetCategory(int id)
        {
            return _dbContext.Category.FirstOrDefault(c => c.CategoryId == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _dbContext.Category.FirstOrDefault(c => c.CategoryName == name);
        }
    }
}
