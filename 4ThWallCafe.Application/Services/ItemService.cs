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
    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;
        private ILogger _logger;
        public ItemService(IItemRepository itemRepository, ILogger<IItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public Result Additem(Item item)
        {
            try
            {
                _itemRepository.Additem(item);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditItem(Item item)
        {
            try
            {
                var duplicate = _itemRepository.GetItemByName(item.ItemName);

                if (duplicate != null && duplicate.ItemId != item.ItemId)
                {
                    return ResultFactory.Fail($"Item with name: {item.ItemName} already exist!");

                }
                _itemRepository.EditItem(item);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Item>> GetAllItems()
        {
            try
            {
                var items = _itemRepository.GetAllItems();
                if(items.Count >= 1)
                {
                    return ResultFactory.Success(items);
                }
                else
                {
                    return ResultFactory.Fail<List<Item>>("Error getting all items");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<Item>>(ex.Message);
            }
        }

        public Result<Item> GetItem(int id)
        {
            try
            {
                var item = _itemRepository.GetItem(id);
                if(item != null)
                {
                    return ResultFactory.Success(item);
                }
                else
                {
                    return ResultFactory.Fail<Item>("No item found with that ID");
                }
                
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Item>(ex.Message);
            }
        }

        public Result<List<Item>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var items = _itemRepository.GetItemsByCategory(categoryId);

                if(items.Count >= 1)
                {
                    return ResultFactory.Success(items);
                }
                else
                {
                    return ResultFactory.Fail<List<Item>>("No Items found for that category.");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<Item>>(ex.Message);
            }
        }
    }
}
