using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.Data.Repositories;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace _4ThWallCafe.Application.Services
{
    public class ItemPriceService : IItemPriceService
    {
        private IItemPriceRepository _itemPriceRepository;
        private ILogger _logger;
        public ItemPriceService(IItemPriceRepository itemPriceRepository, ILogger<ItemPriceService> logger)
        {
            _itemPriceRepository = itemPriceRepository;
            _logger = logger;   
        }
        public Result AddItemPrice(ItemPrice itemPrice)
        {
            try
            {
                _itemPriceRepository.AddItemPrice(itemPrice);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditItemPrice(ItemPrice itemPrice)
        {
            try
            {
                _itemPriceRepository.EditItemPrice(itemPrice);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<ItemPrice>> GetAllActiveItemPrices()
        {
            try
            {
                var itemPrices = _itemPriceRepository.GetAllActiveItemPrices();
                return ResultFactory.Success(itemPrices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<ItemPrice>>(ex.Message);
            }
        }

        public Result<List<ItemPrice>> GetAllItemPrices()
        {
            try
            {
                var itemPrices = _itemPriceRepository.GetAllItemPrices();
                return ResultFactory.Success(itemPrices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<ItemPrice>>(ex.Message);
            }
        }

        public Result<ItemPrice> GetItemPrice(int id)
        {
            try
            {
                var itemPrice = _itemPriceRepository.GetItemPrice(id);

                return itemPrice is null ? ResultFactory.Fail<ItemPrice>($"ItemPrice with ID : {id} not found. ") :
                    ResultFactory.Success(itemPrice);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<ItemPrice>(ex.Message);
            }
        }
    }
}
