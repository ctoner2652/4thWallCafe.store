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

namespace _4ThWallCafe.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger _logger;

        public CartItemService(ICartItemRepository cartItemRepository, ILogger<CartItemService> logger)
        {
            _cartItemRepository = cartItemRepository;
            _logger = logger;
        }

        public Result Additem(CartItem cartItem)
        {
            try
            {
                _cartItemRepository.Additem(cartItem);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result DeleteUsersCart(Guid userSessionID)
        {
            try
            {
                _cartItemRepository.DeleteUsersCart(userSessionID);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<CartItem>> GetUsersCart(Guid userSessionID)
        {
            try
            {
                var cart = _cartItemRepository.GetUsersCart(userSessionID);
                return cart is null ? ResultFactory.Fail<List<CartItem>>($"No Cart Items found for user with ID : {userSessionID} not found. ") :
                    ResultFactory.Success(cart);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<CartItem>>(ex.Message);
            }
        }
    }
}
