using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.MVC.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IServiceFactory _serviceFactory;

        public ManagerController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IActionResult Index()
        {
            var cafeOrderService = _serviceFactory.CreateCafeOrderService();
            var orderItemService = _serviceFactory.CreateOrderItemService();

            var allOrders = cafeOrderService.GetAllCafeOrders().Data;
            var allOrderItems = orderItemService.GetAllOrderItems().Data;

            var model = new ManagerHomeModel();

            var todaysOrders = allOrders.Where(o => o.OrderDate.Date == DateTime.Today).ToList();
            var todaysOrderIds = new HashSet<int>(todaysOrders.Select(o => o.OrderId));

            var filteredOrderItems = allOrderItems.Where(oi => todaysOrderIds.Contains(oi.OrderId)).ToList();
            decimal? todaySales = 0;
            if(todaysOrders != null)
            {
                foreach(var order in todaysOrders)
                {
                    todaySales += order.AmountDue;
                }
            }
            var monthlyOrders = allOrders.Where(o => o.OrderDate.Date > DateTime.Today.AddMonths(-1)).ToList();
            var monthlyOrderIds = new HashSet<int>(monthlyOrders.Select(o => o.OrderId));

            var monthlyOrderItems = allOrderItems.Where(oi => monthlyOrderIds.Contains(oi.OrderId)).ToList();
            decimal? monthlySales = 0;
            if (monthlyOrders != null)
            {
                foreach (var order in monthlyOrders)
                {
                    monthlySales += order.AmountDue;
                }
            }

            model.allTimeOrders = allOrders.Count;
            model.todayOrders = todaysOrders.Count;
            model.todaySales = todaySales;
            model.monthlySales = monthlySales;
            model.employeeOfMonth = "Melissa Jerina";

            return View(model);
        }


    }
}
