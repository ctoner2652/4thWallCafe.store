using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class CafeOrderRepository : ICafeOrderRepository
    {

        private FourthWallCafeContext _dbContext;

        public CafeOrderRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCafeOrder(CafeOrder cafeOrder)
        {
            _dbContext.CafeOrder.Add(cafeOrder);
            _dbContext.SaveChanges();
        }

        public void EditCafeOrder(CafeOrder cafeOrder)
        {
            _dbContext.CafeOrder.Update(cafeOrder);
            _dbContext.SaveChanges();
        }

        public List<CafeOrder> GetAllCafeOrders()
        {
            return _dbContext.CafeOrder.ToList();
        }

        public CafeOrder GetCafeOrder(int id)
        {
            return _dbContext.CafeOrder.FirstOrDefault(co => co.OrderId == id)!;
        }
    }
}
