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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IServiceFactory _serviceFactory;
        public CategoryController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _categoryService = _serviceFactory.CreateCategoryService();
        }

        /// <summary>
        /// List all Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
        public IActionResult GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }
        /// <summary>
        /// View a Category
        /// </summary>
        /// <param name="id"></param>

        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int id)
        {
            var result = _categoryService.GetCategory(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if(result.Message.Contains("Category with ID"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add a Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCategory(AddCategory category)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category
                {
                    CategoryName = category.CategoryName
                };

                var result = _categoryService.AddCategory(entity);

                if (result.Ok)
                {
                    return Created("", entity);
                }

                if(result.Message.Contains("Category with name"))
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
        /// Edit a Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditCategory(int id, EditCategory category)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    CategoryId = category.CategoryID,
                    CategoryName = category.CategoryName
                };

                var result = _categoryService.EditCategory(entity);

                if (result.Ok)
                {
                    return NoContent();
                }

                if (result.Message.Contains("Category with name"))
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
