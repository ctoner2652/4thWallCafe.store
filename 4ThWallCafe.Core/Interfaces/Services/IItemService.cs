using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface IItemService
    {
        Result<Item> GetItem(int id);
        Result<List<Item>> GetItemsByCategory(int categoryId);
        Result EditItem(Item item);
        Result Additem(Item item);
        Result<List<Item>> GetAllItems();
    }
}
