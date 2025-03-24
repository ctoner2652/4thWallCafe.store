using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class CategoryRepoMock : ICategoryRepository
    {
        private readonly List<Category> _categories;

        public CategoryRepoMock()
        {
            _categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Beverages" },
                new Category { CategoryId = 2, CategoryName = "Desserts" },
                new Category { CategoryId = 3, CategoryName = "Appetizers" }
            };
        }

        public void AddCategory(Category category)
        {
            category.CategoryId = 4;
            _categories.Add(category);
        }

        public void EditCategory(Category category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
            }
        }

        public List<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategory(int id)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _categories.FirstOrDefault(c => c.CategoryName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
