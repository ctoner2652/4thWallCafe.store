using _4ThWallCafe.API.Model;
using _4ThWallCafe.Application.Services;
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
    public class ItemPriceController : Controller
    {
        private IItemPriceService _itemPriceService;
        private readonly IServiceFactory _serviceFactory;
        public ItemPriceController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _itemPriceService = _serviceFactory.CreateItemPriceService();
        }

        /// <summary>
        /// List all ItemPrices
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ItemPrice>), StatusCodes.Status200OK)]
        public IActionResult GetAllItemPrices()
        {
            var result = _itemPriceService.GetAllItemPrices();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List all Active ItemPrices
        /// </summary>
        /// <returns></returns>
        [HttpGet("active")]
        [ProducesResponseType(typeof(List<ItemPrice>), StatusCodes.Status200OK)]
        public IActionResult GetAllActiveItemPrices()
        {
            var result = _itemPriceService.GetAllActiveItemPrices();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// View a ItemPrice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetItemPrice(int id)
        {
            var result = _itemPriceService.GetItemPrice(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("ItemPrice with ID"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add a ItemPrice
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddItemPrice(AddItemPrice itemPrice)
        {
            if (ModelState.IsValid)
            {
                var entity = new ItemPrice
                {
                    ItemId = itemPrice.ItemId,
                    TimeOfDayId = itemPrice.TimeOfDayId,
                    Price = itemPrice.Price,
                    StartDate = itemPrice.StartDate,
                    EndDate = itemPrice.EndDate,
                };

                var result = _itemPriceService.AddItemPrice(entity);

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
        /// Edit a ItemPrice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemPrice"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditItemPrice(int id, EditItemPrice itemPrice)
        {
            if (ModelState.IsValid)
            {
                var entity = new ItemPrice
                {
                    ItemPriceId = itemPrice.ItemPriceID,
                    ItemId = itemPrice.ItemId,
                    TimeOfDayId = itemPrice.TimeOfDayId,
                    Price = itemPrice.Price,
                    StartDate = itemPrice.StartDate,
                    EndDate = itemPrice.EndDate,
                };

                var result = _itemPriceService.EditItemPrice(entity);

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
