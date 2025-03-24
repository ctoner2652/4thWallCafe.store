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
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IServiceFactory _serviceFactory;
        public PaymentTypeController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _paymentTypeService = _serviceFactory.CreatePaymentTypeService();
        }

        /// <summary>
        /// List all Payment Types
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<PaymentType>), StatusCodes.Status200OK)]
        public IActionResult GetAllCategories()
        {
            var result = _paymentTypeService.GetAllPaymentTypes();
            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }
        /// <summary>
        /// View a Payment Type
        /// </summary>
        /// <param name="id"></param>

        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int id)
        {
            var result = _paymentTypeService.GetPaymentTypeByID(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("No Payment Type Found"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }
    }
}
