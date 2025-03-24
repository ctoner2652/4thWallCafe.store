using _4ThWallCafe.MVC.API.Implementations;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace _4ThWallCafe.MVC.Utility
{
    public static class HelperMethods 
    {
        public static async Task<List<MenuItem>> BuildMenuItemsAsync(IItemPriceAPIClient _itemPriceAPIClient, 
            ICategoryAPIClient _categoryAPIClient, ITimeOfDayAPIClient _timeOfDayAPIClient, IItemAPIClient _itemAPIClient, int timeOfDayInt = -1)
        {
            var itemPrices = await _itemPriceAPIClient.GetAllActiveItemPrices();
            var categories = await _categoryAPIClient.GetAllCategoriesAsync();
            var timesOfDay = await _timeOfDayAPIClient.GetAllTimesOfDay();
            var items = await _itemAPIClient.GetAllItems();
            if(timeOfDayInt != -1)
            {
                itemPrices = itemPrices.Where(ip => ip.TimeOfDayId == timeOfDayInt).ToList();
            }            
            var menuItems = new List<MenuItem>();
            foreach (var itemPrice in itemPrices)
            {
                var item = items.FirstOrDefault(i => i.ItemId == itemPrice.ItemId);
                menuItems.Add(new MenuItem
                {
                    ItemID = itemPrice.ItemId,
                    Price = itemPrice.Price,
                    StartDate = itemPrice.StartDate,
                    EndDate = itemPrice.EndDate,
                    ItemName = item.ItemName,
                    ItemPriceID = itemPrice.ItemPriceId,
                    ItemDescription = item.ItemDescription,
                    CategoryName = categories.FirstOrDefault(c => c.CategoryId == item.CategoryId)?.CategoryName,
                    TimeOfDayName = timesOfDay.FirstOrDefault(t => t.TimeOfDayId == itemPrice.TimeOfDayId)?.TimeOfDayName
                });
            }
            return menuItems;
        }

        public static async Task<List<DisplayCartItem>> BuildCartDisplayItemsAsync(ICartItemAPIClient _cartItemAPIClient, 
            IItemAPIClient _itemAPIClient, Guid userSessionId)
        {
            var userCartItems = await _cartItemAPIClient.GetUsersCartAsync(userSessionId);
            var itemList = await _itemAPIClient.GetAllItems();

            return userCartItems.Select(ci => new DisplayCartItem
            {
                ItemId = ci.ItemId,
                ItemName = itemList.FirstOrDefault(i => i.ItemId == ci.ItemId)?.ItemName,
                Quantity = ci.Quantity,
                TotalPrice = ci.TotalPrice,
                UserSessionId = userSessionId
            }).ToList();
        }

        public static async Task<int> GetCurrentTimeOfDayID(ITimeOfDayAPIClient _timeOfDayAPIClient)
        {
            var currentTime = DateTime.Now.Hour;
            int result;

            switch (currentTime)
            {
                case < 10:
                    result = 1; // Breakfast
                    break;
                case >= 10 and < 15:
                    result = 2; // Lunch
                    break;
                case >= 15 and < 19:
                    result = 4; // Dinner
                    break;
                case >= 19 and < 22:
                    result = 3; // Happy Hour
                    break;
                default:
                    result = 4; // Dinner (for hours >= 22)
                    break;
            }

            return result;
        }

        public static async Task AddOrderItemsFromCart(ICartItemAPIClient _cartItemAPIClient, IOrderItemAPIClient orderItemAPIClient,
            Guid userSessionID, int orderID)
        {
            var cartItems = await _cartItemAPIClient.GetUsersCartAsync(userSessionID);
            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cartItems)
            {
                var entity = new OrderItem
                {
                    OrderId = orderID,
                    ExtendedPrice = cartItem.TotalPrice,
                    ItemPriceId = cartItem.ItemPriceId, 
                    Quantity = (byte)cartItem.Quantity,
                };
                orderItems.Add(entity);
            }
            foreach(var orderItem in orderItems)
            {
                await orderItemAPIClient.AddOrderItemAsync(orderItem);
            }
        }
    }
}
