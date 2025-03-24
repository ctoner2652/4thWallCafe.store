using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface ICartItemAPIClient
    {
        Task<List<CartItem>> GetUsersCartAsync(Guid userSessionID);
        Task AdditemAsync(CartItem cartItem);
        Task DeleteUsersCartAsync(Guid userSessionID);
    }
}
