using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class CafeOrderRepoMock : ICafeOrderRepository
    {
        private readonly List<CafeOrder> _orders;

        public CafeOrderRepoMock()
        {
            _orders = new List<CafeOrder>
            {
                new CafeOrder { OrderId = 1, ServerId = 101, OrderDate = DateTime.Now.AddDays(-1), AmountDue = 25.99m },
                new CafeOrder { OrderId = 2, ServerId = 102, OrderDate = DateTime.Now.AddDays(-3), AmountDue = 15.49m },
                new CafeOrder { OrderId = 3, ServerId = 103, OrderDate = DateTime.Now.AddDays(-7), AmountDue = 32.99m }
            };
        }

        public void AddCafeOrder(CafeOrder cafeOrder)
        {
            cafeOrder.OrderId = 4;
            _orders.Add(cafeOrder);
        }

        public void EditCafeOrder(CafeOrder cafeOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == cafeOrder.OrderId);
            if (existingOrder != null)
            {
                existingOrder.ServerId = cafeOrder.ServerId;
                existingOrder.OrderDate = cafeOrder.OrderDate;
                existingOrder.AmountDue = cafeOrder.AmountDue;
            }
        }

        public List<CafeOrder> GetAllCafeOrders()
        {
            return _orders;
        }

        public CafeOrder GetCafeOrder(int id)
        {
            return _orders.FirstOrDefault(o => o.OrderId == id);
        }
    }
}
