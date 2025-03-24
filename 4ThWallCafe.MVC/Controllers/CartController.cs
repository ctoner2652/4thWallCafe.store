using _4ThWallCafe.MVC.API.Implementations;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using _4ThWallCafe.MVC.Utility;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.MVC.Controllers
{
    public class CartController : Controller
    {


        private  readonly IApiClientFactory _clientFactory;
        private readonly ILogger _logger;
        public CartController(IApiClientFactory factory, ILogger<CartController> logger)
        {
            _clientFactory = factory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddToCart(string id, CartItem model)
        {
            var _itemPriceAPIClient = await _clientFactory.CreateItemPriceAPIClient();
            var _cartItemAPIClient = await _clientFactory.CreateCartItemClient();
            var cartId = Request.Cookies["CartId"];
            if (string.IsNullOrEmpty(cartId))
            {
                // This theoretically shouldn't happen if the middleware is in place
                _logger.LogCritical("Major error in handling CartId, really only possible if cookies are manually cleared");
                return BadRequest("No CartId cookie found.");
            }
            var timeofDayID = 0;
            switch (id)
            {
                case "Breakfast": timeofDayID = 1;
                    break;
                case "Lunch": timeofDayID = 2;
                    break;
                case "Dinner": timeofDayID = 4;
                    break;
                case "Happy Hour": timeofDayID = 3;
                    break;
            }
            var itemPrices = await _itemPriceAPIClient.GetAllItemPrices();
            var itemPrice = itemPrices.FirstOrDefault(ip => ip.ItemId == model.ItemId && ip.TimeOfDayId == timeofDayID);
            var userSessionId = Guid.Parse(cartId);

            var entity = new CartItem()
            {
                UserSessionId = userSessionId,
                Quantity = model.Quantity,
                ItemId = model.ItemId,
                ItemPriceId = itemPrice.ItemPriceId,
                TotalPrice = itemPrice.Price * model.Quantity
            };

            await _cartItemAPIClient.AdditemAsync(entity);
            TempData["Message"] = $"{entity.Quantity} items have been added to the cart!";
            return RedirectToAction("GetOrder", "Menu");
        }

        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            var _cartItemAPIClient = await _clientFactory.CreateCartItemClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var cartId = Request.Cookies["CartId"];
            if (string.IsNullOrEmpty(cartId))
            {
                // This theoretically shouldn't happen if the middleware is in place
                return BadRequest("No CartId cookie found.");
            }

            var userSessionId = Guid.Parse(cartId);

            var cartItemList = await _cartItemAPIClient.GetUsersCartAsync(userSessionId);

            var itemList = await _itemAPIClient.GetAllItems();

            var model = await HelperMethods.BuildCartDisplayItemsAsync(_cartItemAPIClient, _itemAPIClient, userSessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EmptyCart()
        {
            var cartId = Request.Cookies["CartId"];
            if (string.IsNullOrEmpty(cartId))
            {
                // This theoretically shouldn't happen if the middleware is in place
                return BadRequest("No CartId cookie found.");
            }

            var userSessionId = Guid.Parse(cartId);

            var _cartItemAPIClient = await _clientFactory.CreateCartItemClient();
            try
            {
                await _cartItemAPIClient.DeleteUsersCartAsync(userSessionId);
                TempData["Message"] = "Cart has been emptied!";
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("ViewCart");
            }
            
        }
    }
}
