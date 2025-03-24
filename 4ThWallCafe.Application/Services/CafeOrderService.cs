using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.Extensions.Logging;

namespace _4ThWallCafe.Application.Services
{
    public class CafeOrderService : ICafeOrderService
    {
        private readonly ICafeOrderRepository _cafeOrderReposistory;
        private readonly ILogger _logger;
        public CafeOrderService(ICafeOrderRepository cafeOrderReposistory, ILogger<CafeOrderService> logger)
        {
            _cafeOrderReposistory = cafeOrderReposistory;
            _logger = logger;
        }

        public Result AddCafeOrder(CafeOrder cafeOrder)
        {
            try
            {
                _cafeOrderReposistory.AddCafeOrder(cafeOrder);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditCafeOrder(CafeOrder cafeOrder)
        {
            try
            {
                _cafeOrderReposistory.EditCafeOrder(cafeOrder);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<CafeOrder>> GetAllCafeOrders()
        {
            try
            {
                var orders = _cafeOrderReposistory.GetAllCafeOrders();
                if(orders.Count >= 1)
                {
                    return ResultFactory.Success(orders);
                }
                else
                {
                    return ResultFactory.Fail<List<CafeOrder>>("Error getting all orders!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<CafeOrder>>(ex.Message);
            }
        }

        public Result<CafeOrder> GetCafeOrder(int id)
        {
            try
            {
                var order = _cafeOrderReposistory.GetCafeOrder(id);
                return order is null ? ResultFactory.Fail<CafeOrder>($"Order with ID : {id} not found. ") :
                    ResultFactory.Success(order);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<CafeOrder>(ex.Message);
            }
        }
    }
}
