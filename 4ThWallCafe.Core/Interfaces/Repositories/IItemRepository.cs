using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Item GetItem(int id);
        List<Item> GetItemsByCategory(int categoryId);
        List<Item> GetAllItems();
        void EditItem(Item item);
        void Additem(Item item);
        Item GetItemByName(string name);
    }
}
