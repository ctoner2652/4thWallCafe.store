using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface ICafeOrderRepository
    {
        CafeOrder GetCafeOrder(int id);
        List<CafeOrder> GetAllCafeOrders();
        void EditCafeOrder(CafeOrder cafeOrder);
        void AddCafeOrder(CafeOrder cafeOrder);
    }
}
