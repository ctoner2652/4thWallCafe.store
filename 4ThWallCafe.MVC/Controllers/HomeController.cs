using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;
            _logger.LogError(exception, "Unhandled exception occurred.");
            return View();
        }
    }
}
