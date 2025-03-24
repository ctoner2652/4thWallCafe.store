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
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IServiceFactory _serviceFactory;
        public ItemController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _itemService = _serviceFactory.CreateItemService();
        }
        /// <summary>
        /// List all Items
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        public IActionResult GetAllCategories()
        {
            var result = _itemService.GetAllItems();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List all items by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpGet("{categoryID}/category")]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        public IActionResult GetAllItemsByCategory(int categoryID)
        {
            var result = _itemService.GetItemsByCategory(categoryID);

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }
        /// <summary>
        /// View a Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetItem(int id)
        {
            var result = _itemService.GetItem(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("Item with ID"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add a Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddItem(AddItem item)
        {
            if (ModelState.IsValid)
            {
                var entity = new Item
                {
                    ItemName = item.ItemName,
                    CategoryId = item.CategoryId,
                    ItemDescription = item.ItemDescription
                };

                var result = _itemService.Additem(entity);

                if (result.Ok)
                {
                    return Created("", entity);
                }

                if (result.Message.Contains("Item with name"))
                {
                    return Conflict(result.Message);
                }

                return StatusCode(500, result.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Edit a Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditItem(int id, EditItem item)
        {
            if (ModelState.IsValid)
            {
                var entity = new Item()
                {
                    ItemId = item.ItemId,
                    CategoryId = item.CategoryId,
                    ItemDescription = item.ItemDescription,
                    ItemName = item.ItemName

                };

                var result = _itemService.EditItem(entity);

                if (result.Ok)
                {
                    return NoContent();
                }

                if (result.Message.Contains("Item with name"))
                {
                    return Conflict(result.Message);
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
