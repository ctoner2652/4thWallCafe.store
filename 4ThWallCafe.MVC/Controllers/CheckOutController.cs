using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using _4ThWallCafe.MVC.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _4ThWallCafe.MVC.Controllers
{
    public class CheckOutController : Controller
    {

        private readonly IApiClientFactory _clientFactory;
        public CheckOutController(IApiClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var _cartItemAPIClient = await _clientFactory.CreateCartItemClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var _paymentTypeAPIClient = await _clientFactory.CreatePaymentTypeClient();
            var cartId = Request.Cookies["CartId"];
            if (string.IsNullOrEmpty(cartId))
            {
                // This theoretically shouldn't happen if the middleware is in place
                return BadRequest("No CartId cookie found.");
            }
            var userSessionId = Guid.Parse(cartId);
            var cartItemList = await HelperMethods.BuildCartDisplayItemsAsync(_cartItemAPIClient, _itemAPIClient, userSessionId);
            var payMentTypes = await _paymentTypeAPIClient.GetAllPaymentTypesAsync();
            SelectList payments = new SelectList(payMentTypes, "PaymentTypeId", "PaymentTypeName");
            decimal subTotal = 0;
            foreach (var item in cartItemList)
            {
                subTotal += item.TotalPrice;
            }
            var model = new CheckOutForm
            {
                cartItems = cartItemList,
                PaymentTypes = payments,
                SubTotal = subTotal,
                Tax = subTotal * 0.20M,
                AmountDue = subTotal + (subTotal * 0.20M)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(CheckOutForm model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CheckOut");
            }
            if(model.Tip == null)
            {
                model.Tip = 0;
            }
            var _cafeOrderAPIClient = await _clientFactory.CreateCafeOrderClient();
            var entity = new CafeOrder
            {
                ServerId = null,
                PaymentTypeId = model.PaymentTypeID,
                OrderDate = DateTime.Today,
                SubTotal = model.SubTotal,
                Tax = model.Tax,
                Tip = model.Tip,
                AmountDue = model.AmountDue + model.Tip
            };
            var createdOrder = await _cafeOrderAPIClient.AddCafeOrderAsync(entity);
            return RedirectToAction("OrderConfirmationPage", new { id = createdOrder.OrderId });
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmationPage(int id)
        {
            var _cartItemAPIClient = await _clientFactory.CreateCartItemClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var _paymentTypeAPIClient = await _clientFactory.CreatePaymentTypeClient();
            var _cafeOrderAPIClient = await _clientFactory.CreateCafeOrderClient();
            var _orderItemAPIClient = await _clientFactory.CreateOrderItemClient();
            var order = await _cafeOrderAPIClient.GetCafeOrderAsync(id);
            var cartId = Request.Cookies["CartId"];
            if (string.IsNullOrEmpty(cartId))
            {
                // This theoretically shouldn't happen if the middleware is in place
                return BadRequest("No CartId cookie found.");
            }
            var userSessionId = Guid.Parse(cartId);
            var cartItemList = await HelperMethods.BuildCartDisplayItemsAsync(_cartItemAPIClient, _itemAPIClient, userSessionId);
            await HelperMethods.AddOrderItemsFromCart(_cartItemAPIClient, _orderItemAPIClient, userSessionId, id);
            await _cartItemAPIClient.DeleteUsersCartAsync(userSessionId);
            var paymentType = await _paymentTypeAPIClient.GetPaymentTypeByIDAsync(order.PaymentTypeId);
            var model = new OrderConfirmationInformation
            {
                OrderNumber = order.OrderId,
                ETA = DateTime.Now.AddMinutes(15),
                OrderSummary = cartItemList,
                TotalAmountPaid = order.AmountDue,
                Payment = paymentType.PaymentTypeName
            };
            return View(model);
        }
    }
}
