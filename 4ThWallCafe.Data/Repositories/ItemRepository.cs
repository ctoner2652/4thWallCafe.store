using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace _4ThWallCafe.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private FourthWallCafeContext _dbContext;

        public ItemRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Item> GetAllItems()
        {
            return _dbContext.Item.ToList();
        }

        public void Additem(Item item)
        {
            _dbContext.Item.Add(item);
            _dbContext.SaveChanges();
        }

        public void EditItem(Item item)
        {
            _dbContext.Item.Update(item);
            _dbContext.SaveChanges();
        }

        public Item GetItem(int id)
        {
            return _dbContext.Item.AsNoTracking().FirstOrDefault(i => i.ItemId == id);
        }

        public Item GetItemByName(string name)
        {
            return _dbContext.Item.AsNoTracking().FirstOrDefault(i => i.ItemName == name);
        }

        public List<Item> GetItemsByCategory(int categoryId)
        {
            return _dbContext.Item.Where(i => i.CategoryId == categoryId).ToList();
        }
    }
}
