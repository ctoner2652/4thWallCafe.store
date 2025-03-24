using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private FourthWallCafeContext _dbContext;

        public OrderItemRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _dbContext.OrderItem.Add(orderItem);
            _dbContext.SaveChanges();
        }

        public void EditOrderItem(OrderItem orderItem)
        {
            _dbContext.Update(orderItem);
            _dbContext.SaveChanges();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return _dbContext.OrderItem.ToList();
        }

        public OrderItem GetOrderItem(int id)
        {
            return _dbContext.OrderItem.FirstOrDefault(oi => oi.OrderItemId == id)!;
        }
    }
}
