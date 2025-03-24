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
    public class CafeOrderController : Controller
    {
        private readonly ICafeOrderService _cafeOrderService;
        private readonly IServiceFactory _serviceFactory;
        public CafeOrderController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _cafeOrderService = _serviceFactory.CreateCafeOrderService();
        }

        /// <summary>
        /// List all Cafe Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CafeOrder>), StatusCodes.Status200OK)]
        public IActionResult GetAllCafeOrders()
        {
            var result = _cafeOrderService.GetAllCafeOrders();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }
        /// <summary>
        /// View a Cafe Order
        /// </summary>
        /// <param name="id"></param>

        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CafeOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCafeOrder(int id)
        {
            var result = _cafeOrderService.GetCafeOrder(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("Order with ID"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add a Cafe Order
        /// </summary>
        /// <param name="cafeorder"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCafeOrder(AddCafeOrder cafeOrder)
        {
            if (ModelState.IsValid)
            {
                var entity = new CafeOrder
                {
                    ServerId = cafeOrder.ServerId,
                    Tax = Math.Round(cafeOrder.Tax ?? 0, 2),
                    AmountDue = Math.Round(cafeOrder.AmountDue ?? 0, 2),
                    SubTotal = Math.Round(cafeOrder.SubTotal ?? 0, 2),
                    Tip = Math.Round(cafeOrder.Tip ?? 0, 2),
                    OrderDate = cafeOrder.OrderDate,
                    PaymentTypeId = cafeOrder.PaymentTypeId
                };

                var result = _cafeOrderService.AddCafeOrder(entity);

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
        /// Edit a Cafe Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cafeOrder"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditCafeOrder(int id, EditCafeOrder cafeOrder)
        {
            if (ModelState.IsValid)
            {
                var entity = new CafeOrder()
                {
                    OrderId = cafeOrder.OrderId,
                    ServerId = cafeOrder.ServerId,
                    Tax = cafeOrder.Tax,
                    AmountDue = cafeOrder.AmountDue,
                    SubTotal = cafeOrder.SubTotal,
                    Tip = cafeOrder.Tip,
                    OrderDate = cafeOrder.OrderDate,
                    PaymentTypeId = cafeOrder.PaymentTypeId
                };

                var result = _cafeOrderService.EditCafeOrder(entity);

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
