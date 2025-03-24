using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _4ThWallCafe.MVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ItemController : Controller
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ILogger _logger;
        public ItemController(IServiceFactory serviceFactory, ILogger<ItemController> logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetItems()
        {
            var itemService = _serviceFactory.CreateItemService();
            var itemPriceService = _serviceFactory.CreateItemPriceService();
            var categoryService = _serviceFactory.CreateCategoryService();
            var timeOfDayService = _serviceFactory.CreateTimeOfDayService();

            var itemResult = itemService.GetAllItems();
            var itemPriceResult = itemPriceService.GetAllItemPrices();
            var categoryResult = categoryService.GetAllCategories();
            var timeOfDayResult = timeOfDayService.GetAllTimesOfDay();

            var items = new List<Item>();
            var itemPrices = new List<ItemPrice>();
            var categories = new List<Category>();
            var timesOfDay = new List<TimeOfDay>();
            if (itemResult.Ok)
            {
                items = itemResult.Data;
                if (itemPriceResult.Ok)
                {
                    itemPrices = itemPriceResult.Data;

                    if (categoryResult.Ok)
                    {
                        categories = categoryResult.Data;

                        if (timeOfDayResult.Ok)
                        {
                            timesOfDay = timeOfDayResult.Data;


                            var model = new List<ManagementMenuItem>();

                            foreach(var item in items)
                            {
                                var itemPricesOfItem = itemPrices.Where(ip => ip.ItemId == item.ItemId).ToList();

                                foreach(var itemPrice in itemPricesOfItem)
                                {
                                    var entity = new ManagementMenuItem
                                    {
                                        ItemName = item.ItemName,
                                        ItemDescription = item.ItemDescription,
                                        CategoryName = categories.FirstOrDefault(c => c.CategoryId == item.CategoryId).CategoryName,
                                        Price = itemPrice.Price,
                                        TimeOfDayName = timesOfDay.FirstOrDefault(t => t.TimeOfDayId == itemPrice.TimeOfDayId).TimeOfDayName,
                                        StartDate = itemPrice.StartDate,
                                        EndDate = itemPrice.EndDate,
                                        ItemPriceID = itemPrice.ItemPriceId,
                                    };

                                    model.Add(entity);
                                }
                                
                            }

                            return View(model);

                        }
                    }
                }
            }

            _logger.LogCritical("Error accessing service data");
            return RedirectToAction("Index", "Manager");
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            var categoryService = _serviceFactory.CreateCategoryService();
            var timeOfDayService = _serviceFactory.CreateTimeOfDayService();

            var categoryResult = categoryService.GetAllCategories();
            var timeOfDayResult = timeOfDayService.GetAllTimesOfDay();

            var model = new CreateNewItem
            {
                Categories = categoryResult.Ok
                    ? new SelectList(categoryResult.Data, "CategoryId", "CategoryName")
                    : new SelectList(new List<Category>()),

                TimesOfDay = timeOfDayResult.Ok
                    ? new SelectList(timeOfDayResult.Data, "TimeOfDayId", "TimeOfDayName")
                    : new SelectList(new List<TimeOfDay>())
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem(CreateNewItem model)
        {
            if (!ModelState.IsValid)
            {
                var categoryService = _serviceFactory.CreateCategoryService();
                var timeOfDayService = _serviceFactory.CreateTimeOfDayService();

                model.Categories = new SelectList(categoryService.GetAllCategories().Data, "CategoryId", "CategoryName");
                model.TimesOfDay = new SelectList(timeOfDayService.GetAllTimesOfDay().Data, "TimeOfDayId", "TimeOfDayName");

                return View(model);
            }

            var itemService = _serviceFactory.CreateItemService();
            var itemPriceService = _serviceFactory.CreateItemPriceService();

            var newItem = new Item
            {
                ItemName = model.ItemName,
                ItemDescription = model.ItemDescription,
                CategoryId = model.CategoryId
            };

            var itemResult = itemService.Additem(newItem);

            if (itemResult.Ok)
            {
                var newItemPrice = new ItemPrice
                {
                    ItemId = newItem.ItemId,
                    TimeOfDayId = model.TimeOfDayId,
                    Price = model.Price,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                var itemPriceResult = itemPriceService.AddItemPrice(newItemPrice);

                if (itemPriceResult.Ok)
                {
                    TempData["Message"] = "New Item has been succesfully added!";
                    return RedirectToAction("GetItems");
                }
                else
                {
                    ModelState.AddModelError("", $"{itemResult.Message}");
                }

                
            }
            else
            {
                ModelState.AddModelError("", $"{itemResult.Message}");
            }

            var categoryServiceRetry = _serviceFactory.CreateCategoryService();
            var timeOfDayServiceRetry = _serviceFactory.CreateTimeOfDayService();

            model.Categories = new SelectList(categoryServiceRetry.GetAllCategories().Data, "CategoryId", "CategoryName");
            model.TimesOfDay = new SelectList(timeOfDayServiceRetry.GetAllTimesOfDay().Data, "TimeOfDayId", "TimeOfDayName");

            return View(model);
        }
        [HttpGet]
        public IActionResult EditItem(int ItemPriceID)
        {
            var itemService = _serviceFactory.CreateItemService();
            var itemPriceService = _serviceFactory.CreateItemPriceService();
            var categoryService = _serviceFactory.CreateCategoryService();
            var timeOfDayService = _serviceFactory.CreateTimeOfDayService();

            
            var itemPriceResult = itemPriceService.GetItemPrice(ItemPriceID);
            if (!itemPriceResult.Ok)
            {
                return RedirectToAction("GetItems");
            }
            var itemPrice = itemPriceResult.Data;
            var categoryResult = categoryService.GetAllCategories();
            var timeOfDayResult = timeOfDayService.GetAllTimesOfDay();
            var itemResult = itemService.GetItem(itemPrice.ItemId);
            if (!itemResult.Ok)
            {
                return RedirectToAction("GetItems");
            }

            var item = itemResult.Data;
            

            var model = new EditItemModel
            {
                ItemID = item.ItemId,
                ItemPriceID = ItemPriceID,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                CategoryId = item.CategoryId,
                Price = itemPrice?.Price ?? 0,
                TimeOfDayId = itemPrice?.TimeOfDayId ?? 0,
                StartDate = itemPrice?.StartDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = itemPrice?.EndDate,
                Categories = new SelectList(categoryResult.Data, "CategoryId", "CategoryName", item.CategoryId),
                TimesOfDay = new SelectList(timeOfDayResult.Data, "TimeOfDayId", "TimeOfDayName", itemPrice?.TimeOfDayId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(EditItemModel model)
        {
            if (!ModelState.IsValid)
            {
                var categoryService = _serviceFactory.CreateCategoryService();
                var timeOfDayService = _serviceFactory.CreateTimeOfDayService();

                model.Categories = new SelectList(categoryService.GetAllCategories().Data, "CategoryId", "CategoryName", model.CategoryId);
                model.TimesOfDay = new SelectList(timeOfDayService.GetAllTimesOfDay().Data, "TimeOfDayId", "TimeOfDayName", model.TimeOfDayId);

                return View(model);
            }

            var itemService = _serviceFactory.CreateItemService();
            var itemPriceService = _serviceFactory.CreateItemPriceService();

            var existingItem = new Item
            {
                ItemId = model.ItemID,
                ItemName = model.ItemName,
                ItemDescription = model.ItemDescription,
                CategoryId = model.CategoryId
            };

            var updateItemResult = itemService.EditItem(existingItem);

            if (updateItemResult.Ok)
            {
                var existingItemPrice = new ItemPrice
                {
                    ItemPriceId = model.ItemPriceID,
                    ItemId = model.ItemID,
                    TimeOfDayId = model.TimeOfDayId,
                    Price = model.Price,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                var updatePriceResult = itemPriceService.EditItemPrice(existingItemPrice);

                if (updatePriceResult.Ok)
                {
                    TempData["Message"] = "Item has been succesfully updated!";
                    return RedirectToAction("GetItems");
                }

                ModelState.AddModelError("", $"Failed to update item price. Error : {updatePriceResult.Message}");
            }
            else
            {
                ModelState.AddModelError("", $"Failed to update item. Error :{updateItemResult.Message}");
            }

            var categoryServiceRetry = _serviceFactory.CreateCategoryService();
            var timeOfDayServiceRetry = _serviceFactory.CreateTimeOfDayService();

            model.Categories = new SelectList(categoryServiceRetry.GetAllCategories().Data, "CategoryId", "CategoryName", model.CategoryId);
            model.TimesOfDay = new SelectList(timeOfDayServiceRetry.GetAllTimesOfDay().Data, "TimeOfDayId", "TimeOfDayName", model.TimeOfDayId);

            return View(model);
        }
    }
}
