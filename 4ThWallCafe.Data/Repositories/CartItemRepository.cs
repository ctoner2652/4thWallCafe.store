using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private FourthWallCafeContext _dbContext;

        public CartItemRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Additem(CartItem cartItem)
        {
            _dbContext.CartItem.Add(cartItem);
            _dbContext.SaveChanges();
        }

        public void DeleteUsersCart(Guid userSessionID)
        {
            var cartItems = GetUsersCart(userSessionID);
            foreach (var cartItem in cartItems)
            {
                _dbContext.CartItem.Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }

        public List<CartItem> GetUsersCart(Guid userSessionID)
        {
            return _dbContext.CartItem.Where(ci => ci.UserSessionId == userSessionID).ToList();
        }
    }
}
