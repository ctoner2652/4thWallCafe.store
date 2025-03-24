using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Application;
using _4ThWallCafe.Core.Interfaces.Repositories;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface IServiceFactory
    {
         ICategoryService CreateCategoryService();
         IItemPriceService CreateItemPriceService();
         IItemService CreateItemService();
         ITimeOfDayService CreateTimeOfDayService();
         ICafeOrderService CreateCafeOrderService();
         ICartItemService CreateCartItemService();
         IOrderItemService CreateOrderItemService();
         IPaymentTypeService CreatePaymentTypeService();
         IServerService CreateServerService();
    }
}
