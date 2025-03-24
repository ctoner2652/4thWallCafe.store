using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface IOrderItemAPIClient
    {
        Task<OrderItem> GetOrderItemAsync(int id);
        Task<List<OrderItem>> GetAllOrderItemsAsync();
        Task EditOrderItemAsync(OrderItem orderItem);
        Task AddOrderItemAsync(OrderItem orderItem);
    }
}
