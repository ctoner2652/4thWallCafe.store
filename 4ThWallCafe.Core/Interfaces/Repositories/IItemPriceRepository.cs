using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface IItemPriceRepository
    {
        List<ItemPrice> GetAllActiveItemPrices();
        List<ItemPrice> GetAllItemPrices();
        void EditItemPrice(ItemPrice itemPrice);
        void AddItemPrice(ItemPrice itemPrice); 
        ItemPrice GetItemPrice(int id);
    }
}
