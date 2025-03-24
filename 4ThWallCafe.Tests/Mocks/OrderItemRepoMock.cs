using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class OrderItemRepoMock : IOrderItemRepository
    {
        private readonly List<OrderItem> _orderItems;

        public OrderItemRepoMock()
        {
            _orderItems = new List<OrderItem>
            {
                new OrderItem { OrderItemId = 1, OrderId = 1001, ItemPriceId = 201, Quantity = 2, ExtendedPrice = 19.98m },
                new OrderItem { OrderItemId = 2, OrderId = 1001, ItemPriceId = 202, Quantity = 1, ExtendedPrice = 9.99m },
                new OrderItem { OrderItemId = 3, OrderId = 1002, ItemPriceId = 203, Quantity = 3, ExtendedPrice = 29.97m },
                new OrderItem { OrderItemId = 4, OrderId = 1003, ItemPriceId = 204, Quantity = 1, ExtendedPrice = 14.99m },
                new OrderItem { OrderItemId = 5, OrderId = 1004, ItemPriceId = 205, Quantity = 2, ExtendedPrice = 23.98m }
            };
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            orderItem.OrderItemId = 6;
            _orderItems.Add(orderItem);
        }

        public void EditOrderItem(OrderItem orderItem)
        {
            var existing = _orderItems.FirstOrDefault(oi => oi.OrderItemId == orderItem.OrderItemId);
            if (existing != null)
            {
                existing.OrderId = orderItem.OrderId;
                existing.ItemPriceId = orderItem.ItemPriceId;
                existing.Quantity = orderItem.Quantity;
                existing.ExtendedPrice = orderItem.ExtendedPrice;
            }
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return _orderItems.ToList();
        }

        public OrderItem GetOrderItem(int id)
        {
            return _orderItems.FirstOrDefault(oi => oi.OrderItemId == id);
        }
    }
}
