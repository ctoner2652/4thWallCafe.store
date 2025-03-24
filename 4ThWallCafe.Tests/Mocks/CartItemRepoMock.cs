using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class CartItemRepoMock : ICartItemRepository
    {
        private readonly List<CartItem> _cartItems;

        public CartItemRepoMock()
        {
            // Simulating three different "users" with separate carts
            _cartItems = new List<CartItem>
            {
                // User 1's Cart
                new CartItem { CartItemId = 1, ItemId = 101, Quantity = 2, UserSessionId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
                new CartItem { CartItemId = 2, ItemId = 102, Quantity = 1, UserSessionId = Guid.Parse("11111111-1111-1111-1111-111111111111") },

                // User 2's Cart
                new CartItem { CartItemId = 3, ItemId = 103, Quantity = 3, UserSessionId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
                new CartItem { CartItemId = 4, ItemId = 104, Quantity = 1, UserSessionId = Guid.Parse("22222222-2222-2222-2222-222222222222") },

                // User 3's Cart
                new CartItem { CartItemId = 5, ItemId = 105, Quantity = 4, UserSessionId = Guid.Parse("33333333-3333-3333-3333-333333333333") }
            };
        }

        public void Additem(CartItem cartItem)
        {
            _cartItems.Add(cartItem);
        }

        public void DeleteUsersCart(Guid userSessionID)
        {
            _cartItems.RemoveAll(ci => ci.UserSessionId == userSessionID);
        }

        public List<CartItem> GetUsersCart(Guid userSessionID)
        {
            return _cartItems.Where(ci => ci.UserSessionId == userSessionID).ToList();
        }
    }
}
