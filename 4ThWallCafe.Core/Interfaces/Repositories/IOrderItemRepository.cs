using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface IOrderItemRepository
    {
        OrderItem GetOrderItem(int id);
        List<OrderItem> GetAllOrderItems();
        void EditOrderItem(OrderItem orderItem);
        void AddOrderItem(OrderItem orderItem);
    }
}
