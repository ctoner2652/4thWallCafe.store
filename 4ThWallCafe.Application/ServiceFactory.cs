using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Application.Services;
using _4ThWallCafe.Core.Interfaces.Application;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.Data.Repositories;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.Extensions.Logging;

namespace _4ThWallCafe.Application 
{
    public class ServiceFactory : IServiceFactory
    {
        private FourthWallCafeContext _dbContext;
        private readonly ILoggerFactory _loggerFactory;
        public ServiceFactory(FourthWallCafeContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _loggerFactory = loggerFactory;
        }

        public ICategoryService CreateCategoryService()
        {
            var categoryLogger = _loggerFactory.CreateLogger<CategoryService>();
            return new CategoryService(new CategoryRepository(_dbContext), categoryLogger);
        }

        public IItemPriceService CreateItemPriceService()
        {
            var itemPriceLogger = _loggerFactory.CreateLogger<ItemPriceService>();
            return new ItemPriceService(new ItemPriceRepository(_dbContext), itemPriceLogger);
        }

        public IItemService CreateItemService()
        {
            var itemLogger = _loggerFactory.CreateLogger<ItemService>();
            return new ItemService(new ItemRepository(_dbContext), itemLogger);
        }

        public ITimeOfDayService CreateTimeOfDayService()
        {
            var timeOfDayLogger = _loggerFactory.CreateLogger<TimeOfDayService>();
            return new TimeOfDayService(new TimeOfDayRepository(_dbContext), timeOfDayLogger);
        }

        public ICafeOrderService CreateCafeOrderService()
        {
            var cafeOrderLogger = _loggerFactory.CreateLogger<CafeOrderService>();
            return new CafeOrderService(new CafeOrderRepository(_dbContext), cafeOrderLogger);
        }
        public ICartItemService CreateCartItemService()
        {
            var cartItemLogger = _loggerFactory.CreateLogger<CartItemService>();
            return new CartItemService(new CartItemRepository(_dbContext), cartItemLogger);
        }
        public IOrderItemService CreateOrderItemService()
        {
            var orderItemLogger = _loggerFactory.CreateLogger<OrderItemService>();
            return new OrderItemService(new OrderItemRepository(_dbContext), orderItemLogger);
        }
        public IPaymentTypeService CreatePaymentTypeService()
        {
            var paymentTypeLogger = _loggerFactory.CreateLogger<PaymentTypeService>();    
            return new PaymentTypeService(new PaymentTypeRepository(_dbContext), paymentTypeLogger);
        }
        public IServerService CreateServerService()
        {
            var serverLogger = _loggerFactory.CreateLogger<ServerService>();
            return new ServerService(new ServerRepository(_dbContext), serverLogger);
        }
    }
}
