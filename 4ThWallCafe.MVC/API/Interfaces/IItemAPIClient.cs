using _4ThWallCafe.API.Model;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface IItemAPIClient
    {
        Task<List<Item>> GetItemsByCategory(int categoryID);
        Task<Item> GetItemByID(int id);
        Task AddItem(AddItem item);
        Task EditItem(EditItem item);
        Task<List<Item>> GetAllItems();
    }
}
