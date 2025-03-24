using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class ItemPriceRepoMock : IItemPriceRepository
    {
        private readonly List<ItemPrice> _itemPrices;

        public ItemPriceRepoMock()
        {
   
            _itemPrices = new List<ItemPrice>
            {
                new ItemPrice { ItemPriceId = 1, ItemId = 101, Price = 9.99m, StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-30)), EndDate = null },
                new ItemPrice { ItemPriceId = 2, ItemId = 102, Price = 14.99m, StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)), EndDate = null },
                new ItemPrice { ItemPriceId = 3, ItemId = 103, Price = 7.49m, StartDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-2)), EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)) }, // Expired
                new ItemPrice { ItemPriceId = 4, ItemId = 104, Price = 5.99m, StartDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-1)), EndDate = null },
                new ItemPrice { ItemPriceId = 5, ItemId = 105, Price = 12.99m, StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-15)), EndDate = null }
            };
        }

        public void AddItemPrice(ItemPrice itemPrice)
        {
            itemPrice.ItemPriceId = 6;
            _itemPrices.Add(itemPrice);
        }

        public void EditItemPrice(ItemPrice itemPrice)
        {
            var existing = _itemPrices.FirstOrDefault(ip => ip.ItemPriceId == itemPrice.ItemPriceId);
            if (existing != null)
            {
                existing.Price = itemPrice.Price;
                existing.StartDate = itemPrice.StartDate;
                existing.EndDate = itemPrice.EndDate;
            }
        }

        public List<ItemPrice> GetAllActiveItemPrices()
        {
            return _itemPrices.Where(ip => ip.EndDate == null).ToList();
        }

        public List<ItemPrice> GetAllItemPrices()
        {
            return _itemPrices.ToList();
        }

        public ItemPrice GetItemPrice(int id)
        {
            return _itemPrices.FirstOrDefault(ip => ip.ItemPriceId == id);
        }
    }
}
