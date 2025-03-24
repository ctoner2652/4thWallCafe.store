using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class ItemRepoMock : IItemRepository
    {
        private readonly List<Item> _items;

        public ItemRepoMock()
        {
            _items = new List<Item>
            {
                new Item { ItemId = 101, CategoryId = 1, ItemName = "Cheese Pizza", ItemDescription = "Classic cheese pizza with mozzarella." },
                new Item { ItemId = 102, CategoryId = 1, ItemName = "Pepperoni Pizza", ItemDescription = "Spicy pepperoni on top of cheesy goodness." },
                new Item { ItemId = 103, CategoryId = 2, ItemName = "Veggie Burger", ItemDescription = "Plant-based burger with fresh veggies." },
                new Item { ItemId = 104, CategoryId = 3, ItemName = "Caesar Salad", ItemDescription = "Crisp romaine with parmesan and croutons." },
                new Item { ItemId = 105, CategoryId = 3, ItemName = "Greek Salad", ItemDescription = "Fresh cucumbers, olives, and feta cheese." }
            };
        }

        public void Additem(Item item)
        {
            item.ItemId = 106;
            _items.Add(item);
        }

        public void EditItem(Item item)
        {
            var existing = _items.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (existing != null)
            {
                existing.ItemName = item.ItemName;
                existing.ItemDescription = item.ItemDescription;
                existing.CategoryId = item.CategoryId;
            }
        }

        public List<Item> GetAllItems()
        {
            return _items.ToList();
        }

        public Item GetItem(int id)
        {
            return _items.FirstOrDefault(i => i.ItemId == id);
        }

        public Item GetItemByName(string name)
        {
            return _items.FirstOrDefault(i => i.ItemName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Item> GetItemsByCategory(int categoryId)
        {
            return _items.Where(i => i.CategoryId == categoryId).ToList();
        }
    }
}
