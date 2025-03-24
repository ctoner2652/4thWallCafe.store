using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface IOrderItemService
    {
        Result<OrderItem> GetOrderItem(int id);
        Result<List<OrderItem>> GetAllOrderItems();
        Result EditOrderItem(OrderItem orderItem);
        Result AddOrderItem(OrderItem orderItem);
    }
}
