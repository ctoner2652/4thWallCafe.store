using System.Diagnostics;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using _4ThWallCafe.MVC.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _4ThWallCafe.MVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly IApiClientFactory _clientFactory;

        public MenuController(IApiClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var _timeOfDayAPIClient = await _clientFactory.CreateTimeOfDayClient();
            var _itemPriceAPIClient = await _clientFactory.CreateItemPriceAPIClient();
            var _categoryAPIClient = await _clientFactory.CreateCategoryClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var timeOfDayId = await HelperMethods.GetCurrentTimeOfDayID(_timeOfDayAPIClient);
            var items = await HelperMethods.BuildMenuItemsAsync(_itemPriceAPIClient, _categoryAPIClient, 
                _timeOfDayAPIClient, _itemAPIClient, timeOfDayId);

            var grouped = items
           .GroupBy(i => i.CategoryName)
           .ToDictionary(g => g.Key, g => g.ToList());

            var model = new MenuViewModel
            {
                TimeOfDayName = items.FirstOrDefault()?.TimeOfDayName ?? "Unknown",
                Categories = grouped.Keys.ToList(),
                ItemsByCategory = grouped
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetMenu(int id)
        {
            var _timeOfDayAPIClient = await _clientFactory.CreateTimeOfDayClient();
            var _itemPriceAPIClient = await _clientFactory.CreateItemPriceAPIClient();
            var _categoryAPIClient = await _clientFactory.CreateCategoryClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var items = await HelperMethods.BuildMenuItemsAsync(_itemPriceAPIClient, _categoryAPIClient,
                _timeOfDayAPIClient, _itemAPIClient, id);
            var grouped = items
            .GroupBy(i => i.CategoryName)
            .ToDictionary(g => g.Key, g => g.ToList());

            var model = new MenuViewModel
            {
                TimeOfDayName = items.FirstOrDefault()?.TimeOfDayName ?? "Unknown",
                Categories = grouped.Keys.ToList(),
                ItemsByCategory = grouped
            };



            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(string searchName)
        {
            var _timeOfDayAPIClient = await _clientFactory.CreateTimeOfDayClient();
            var _itemPriceAPIClient = await _clientFactory.CreateItemPriceAPIClient();
            var _categoryAPIClient = await _clientFactory.CreateCategoryClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var timeOfDayId = await HelperMethods.GetCurrentTimeOfDayID(_timeOfDayAPIClient);
            List<MenuItem> items = new();
            if (string.IsNullOrEmpty(searchName))
            {
                 items = await HelperMethods.BuildMenuItemsAsync(_itemPriceAPIClient, _categoryAPIClient,
                _timeOfDayAPIClient, _itemAPIClient, timeOfDayId);
            }
            else
            {
                 items = await HelperMethods.BuildMenuItemsAsync(_itemPriceAPIClient, _categoryAPIClient,
                _timeOfDayAPIClient, _itemAPIClient);
            }
            var model = new GetOrderModel();
            model.Items = items;
            model.TimeOfDayName = items[0].TimeOfDayName ?? "Dinner";
            if (string.IsNullOrEmpty(searchName))
            {
                return View(model);
            }
            else
            {
                model.Items = model.Items.Where(i => i.ItemName.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
                model.TimeOfDayName = items[0].TimeOfDayName ?? "Dinner";
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetOrder(int id)
        {
            var _timeOfDayAPIClient = await _clientFactory.CreateTimeOfDayClient();
            var _itemPriceAPIClient = await _clientFactory.CreateItemPriceAPIClient();
            var _categoryAPIClient = await _clientFactory.CreateCategoryClient();
            var _itemAPIClient = await _clientFactory.CreateItemClient();
            var items = await HelperMethods.BuildMenuItemsAsync(_itemPriceAPIClient, _categoryAPIClient,
                _timeOfDayAPIClient, _itemAPIClient, id);
            var model = new GetOrderModel();
            model.Items = items;
            model.TimeOfDayName = items[0].TimeOfDayName ?? "Dinner";
            return View(model);
        }



    }
}
