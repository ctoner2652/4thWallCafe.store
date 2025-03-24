using _4ThWallCafe.API.Model;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface IItemPriceAPIClient
    {
        Task<List<ItemPrice>> GetAllItemPrices();
        Task<List<ItemPrice>> GetAllActiveItemPrices();
        Task<ItemPrice> GetItemPriceByID(int itemPriceID);
        Task AddItemPrice(AddItemPrice itemPrice);
        Task EditItemPrice(EditItemPrice itemPrice);
    }
}
