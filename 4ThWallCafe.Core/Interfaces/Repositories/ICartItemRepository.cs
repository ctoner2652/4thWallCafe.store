using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        List<CartItem> GetUsersCart(Guid userSessionID);
        void Additem(CartItem cartItem);
        void DeleteUsersCart(Guid userSessionID);
    }
}
