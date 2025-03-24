using _4ThWallCafe.Application.Services;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class TimeOfDayController : Controller
    {
        private ITimeOfDayService _TImeOfDayService;
        private readonly IServiceFactory _serviceFactory;
        public TimeOfDayController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _TImeOfDayService = _serviceFactory.CreateTimeOfDayService();
        }

        /// <summary>
        /// List all Times Of Day
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TimeOfDay>), StatusCodes.Status200OK)]
        public IActionResult GetAllTimesOfDay()
        {
            var result = _TImeOfDayService.GetAllTimesOfDay();

            if (result.Ok)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// View a TimeOfDay
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TimeOfDay), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTimeOfDay(int id)
        {
            var result = _TImeOfDayService.GetTimeOfDayByID(id);

            if (result.Ok)
            {
                return Ok(result.Data);
            }

            if (result.Message.Contains("TimeOfDay with ID"))
            {
                return NotFound(result.Message);
            }

            return StatusCode(500, result.Message);
        }
    }
}
