using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace _4ThWallCafe.Data.Repositories
{
    public class ItemPriceRepository : IItemPriceRepository
    {

        private FourthWallCafeContext _dbContext;

        public ItemPriceRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddItemPrice(ItemPrice itemPrice)
        {
            _dbContext.ItemPrice.Add(itemPrice);
            _dbContext.SaveChanges();
        }

        public void EditItemPrice(ItemPrice itemPrice)
        {
            _dbContext.ItemPrice.Update(itemPrice);
            _dbContext.SaveChanges();
        }

        public List<ItemPrice> GetAllActiveItemPrices()
        {
            return _dbContext.ItemPrice.Where(ip => ip.EndDate == null).ToList();
        }

        public List<ItemPrice> GetAllItemPrices()
        {
            return _dbContext.ItemPrice.ToList();
        }

        public ItemPrice GetItemPrice(int id)
        {
            return _dbContext.ItemPrice.AsNoTracking().FirstOrDefault(ip => ip.ItemPriceId == id);
        }
    }
}
