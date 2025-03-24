using _4ThWallCafe.API.Model;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly IServiceFactory _serviceFactory;
        public CartItemController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _cartItemService = _serviceFactory.CreateCartItemService();
        }

        /// <summary>
        /// List Users Cart Items
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<CartItem>), StatusCodes.Status200OK)]
        public IActionResult GetUsersCart(Guid id)
        {
            var result = _cartItemService.GetUsersCart(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }
        

        /// <summary>
        /// Add a Cart Item
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCartItem(AddCartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                var entity = new CartItem
                {
                    ItemId = cartItem.ItemId,
                    UserSessionId = cartItem.UserSessionId,
                    Quantity = cartItem.Quantity,
                    TotalPrice = cartItem.TotalPrice,
                    ItemPriceId = cartItem.ItemPriceId
                };

                var result = _cartItemService.Additem(entity);

                if (result.Ok)
                {
                    return Created("", entity);
                }

                return StatusCode(500, result.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Delete a Cart For a User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteUsersCart(Guid id)
        {
            var result = _cartItemService.DeleteUsersCart(id);
            if (result.Ok)
            {
                return NoContent();
            }
            return StatusCode(500, result.Message);
        }
    }
}
