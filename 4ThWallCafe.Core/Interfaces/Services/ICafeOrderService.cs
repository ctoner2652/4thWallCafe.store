using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface ICafeOrderService
    {
        Result<CafeOrder> GetCafeOrder(int id);
        Result<List<CafeOrder>> GetAllCafeOrders();
        Result EditCafeOrder(CafeOrder cafeOrder);
        Result AddCafeOrder(CafeOrder cafeOrder);
    }
}
