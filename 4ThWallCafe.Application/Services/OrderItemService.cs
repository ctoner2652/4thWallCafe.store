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
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogger _logger;
        public OrderItemService(IOrderItemRepository orderItemRepository, ILogger<OrderItemService> logger)
        {
            _orderItemRepository = orderItemRepository;
            _logger = logger;
        }

        public Result AddOrderItem(OrderItem orderItem)
        {
            try
            {
                _orderItemRepository.AddOrderItem(orderItem);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditOrderItem(OrderItem orderItem)
        {
            try
            {
                _orderItemRepository.EditOrderItem(orderItem);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<OrderItem>> GetAllOrderItems()
        {
            try
            {
                var orders = _orderItemRepository.GetAllOrderItems();
                if(orders.Count >= 1)
                {
                    return ResultFactory.Success(orders);
                }
                else
                {
                    return ResultFactory.Fail<List<OrderItem>>("Error getting all order items");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<OrderItem>>(ex.Message);
            }
        }

        public Result<OrderItem> GetOrderItem(int id)
        {
            try
            {
                var orderItem = _orderItemRepository.GetOrderItem(id);
                return orderItem is null ? ResultFactory.Fail<OrderItem>($"No OrderItem found with ID : {id}") :
    ResultFactory.Success(orderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<OrderItem>(ex.Message);
            }
        }
    }
}
