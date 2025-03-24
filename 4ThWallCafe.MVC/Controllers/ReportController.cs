using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.API.Implementations;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _4ThWallCafe.MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IApiClientFactory _clientFactory;
        public ReportController(IApiClientFactory factory)
        {
            _clientFactory = factory;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GenerateReport()
        {
            var model = new ReportRequestModel();
            var itemClient = await _clientFactory.CreateItemClient();
            var categoryClient = await _clientFactory.CreateCategoryClient();
            var allItems = await itemClient.GetAllItems();
            var allCategories = await categoryClient.GetAllCategoriesAsync();
            model.items = allItems;
            model.categories = allCategories;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportRequestModel model)
        {
            var displayModel = new DisplayReportModel();
            var orderClient = await _clientFactory.CreateCafeOrderClient();
            var orderItemClient = await _clientFactory.CreateOrderItemClient();
            var itemClient = await _clientFactory.CreateItemClient();
            var itemPriceClient = await _clientFactory.CreateItemPriceAPIClient();
            var categoryClient = await _clientFactory.CreateCategoryClient();

            var allOrders = await orderClient.GetAllCafeOrdersAsync();
            var allOrderItems = await orderItemClient.GetAllOrderItemsAsync();
            var allItems = await itemClient.GetAllItems();
            var allItemPrices = await itemPriceClient.GetAllItemPrices();
            var allCategories = await categoryClient.GetAllCategoriesAsync();

            DateTime startDate = CalculateStartDate(model.TimeRange);

            if(model.CategoryId == null && model.ItemId == null)
            {
                displayModel.Orders = allOrders.Where(o => o.OrderDate >  startDate).ToList();
                displayModel.HasCategory = false;
                displayModel.HasItem = false;
                decimal total = 0;

                foreach (var order in displayModel.Orders)
                {
                    total += (decimal)order.AmountDue;
                }
                displayModel.totalRevenue = total;
                displayModel.title = $"All Orders since {startDate}";
                return View("ReportResults", displayModel);
            }
            if (model.CategoryId == null && model.ItemId != null)
            {
                var filteredItemPrices = allItemPrices.Where(ip => ip.ItemId == model.ItemId).ToList();

                var filteredOrderItems = allOrderItems
                    .Where(oi => filteredItemPrices.Any(ip => ip.ItemPriceId == oi.ItemPriceId))
                    .DistinctBy(o => o.OrderItemId)
                    .ToList();

                var filteredOrders = filteredOrderItems
                    .Select(oi => allOrders.FirstOrDefault(o => o.OrderId == oi.OrderId))
                    .Where(order => order != null && order.OrderDate > startDate)
                    .DistinctBy(o => o.OrderId)
                    .ToList();
                if (filteredOrders == null)
                {
                    TempData["Message"] = "No Orders Found for that critera.";
                    return View("ReportResults", displayModel);
                }
                displayModel.Orders = filteredOrders;
                decimal total = 0;
                foreach (var order in displayModel.Orders)
                {
                    total += (decimal)order.AmountDue;
                }
                displayModel.totalRevenue = total;
                displayModel.HasItem = false;
                displayModel.HasCategory = true;
                var itemName = allItems.First(i => i.ItemId == model.ItemId).ItemName;
                displayModel.title = $"All Orders since {startDate} with {itemName}.";
                return View("ReportResults", displayModel);
            }
            if (model.CategoryId != null && model.ItemId == null)
            {

                var filteredItems = allItems
                    .Where(i => i.CategoryId == model.CategoryId)
                    .ToList();
                var filteredItemPrices = new List<ItemPrice>();
                foreach(var item in filteredItems)
                {
                    filteredItemPrices.AddRange(allItemPrices.Where(ip => ip.ItemId == item.ItemId));
                }
                var filteredOrderItems = allOrderItems
                    .Where(oi => filteredItemPrices.Any(ip => ip.ItemPriceId == oi.ItemPriceId))
                    .DistinctBy(o => o.OrderItemId)
                    .ToList();

                var filteredOrders = filteredOrderItems
                    .Select(oi => allOrders.FirstOrDefault(o => o.OrderId == oi.OrderId)) 
                    .Where(order => order != null && order.OrderDate > startDate) 
                    .DistinctBy(o => o.OrderId) 
                    .ToList();
                if(filteredOrders == null)
                {
                    TempData["Message"] = "No Orders Found for that critera.";
                    return View("ReportResults", displayModel);
                }
                displayModel.Orders = filteredOrders;
                decimal total = 0;
                foreach (var order in displayModel.Orders)
                {
                    total += (decimal)order.AmountDue;
                }
                displayModel.totalRevenue = total;
                displayModel.HasItem = false;
                displayModel.HasCategory = true;
                var categoryName = allCategories.FirstOrDefault(c => c.CategoryId == model.CategoryId).CategoryName;
                displayModel.title = $"All Orders since {startDate} with items from category {categoryName}.";
                return View("ReportResults", displayModel);
            }

            return View("ReportResults", displayModel);
        }


        private DateTime CalculateStartDate(string timeRange)
        {
            return timeRange switch
            {
                "1d" => DateTime.Today.AddDays(-1),
                "1w" => DateTime.Today.AddDays(-7),
                "1m" => DateTime.Today.AddMonths(-1),
                "1y" => DateTime.Today.AddYears(-1),
                "10y" => DateTime.Today.AddYears(-10),
                _ => DateTime.Today
            };
        }

    }
}
