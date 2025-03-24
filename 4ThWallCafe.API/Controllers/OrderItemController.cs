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
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IServiceFactory _serviceFactory;
        public OrderItemController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _orderItemService = _serviceFactory.CreateOrderItemService();
        }

        /// <summary>
        /// List all Order Items
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<OrderItem>), StatusCodes.Status200OK)]
        public IActionResult GetAllOrderItems()
        {
            var result = _orderItemService.GetAllOrderItems();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }
        /// <summary>
        /// View a Order Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int id)
        {
            var result = _orderItemService.GetOrderItem(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("No OrderItem found"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add a Cafe Order
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCategory(AddOrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                var entity = new OrderItem
                {
                    OrderId = orderItem.OrderId,
                    ExtendedPrice = orderItem.ExtendedPrice,
                    Quantity = orderItem.Quantity,
                    ItemPriceId = orderItem.ItemPriceId,
                };

                var result = _orderItemService.AddOrderItem(entity);

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
        /// Edit a Order Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditCategory(int id, EditOrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                var entity = new OrderItem
                {
                    OrderItemId = orderItem.OrderItemId,
                    OrderId = orderItem.OrderId,
                    ExtendedPrice = orderItem.ExtendedPrice,
                    Quantity = orderItem.Quantity,
                    ItemPriceId = orderItem.ItemPriceId,
                };

                var result = _orderItemService.EditOrderItem(entity);

                if (result.Ok)
                {
                    return NoContent();
                }

                return StatusCode(500, result.Message);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
