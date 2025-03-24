using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.MVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ServerController : Controller
    {
        private readonly IServiceFactory _serviceFactory;

        public ServerController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetServers()
        {
            var serverService = _serviceFactory.CreateServerService();

            var serversResult = serverService.GetAllServers();
            List<Server> servers = new List<Server>();
            if (serversResult.Ok)
            {
                servers = serversResult.Data!;
            }
            else
            {
                TempData["Message"] = $"Error getting servers : {serversResult.Message}";
                return RedirectToAction("Index", "Manager");
            }
            return View(servers);
        }

        [HttpGet]
        public IActionResult CreateServer()
        {
            var model = new CreateNewServer();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateServer(CreateNewServer model)
        {
            var serverService = _serviceFactory.CreateServerService();
            if (ModelState.IsValid)
            {
                var entity = new Server
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DoB = model.DoB,
                    HireDate = DateOnly.FromDateTime(DateTime.Now)
                };

                var serverResult = serverService.AddServer(entity);
                if (serverResult.Ok)
                {
                    TempData["Message"] = $"{entity.FirstName},{entity.LastName} has been added succesfully!";
                    return RedirectToAction("GetServers");
                }
                else
                {
                    TempData["Message"] = $"Error adding new server : {serverResult.Message}";
                    return RedirectToAction("GetServers");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EditServer(int id)
        {
            var serverService = _serviceFactory.CreateServerService();
            var serverResult = serverService.GetServer(id);
            var model = new EditServerModel();
            if (serverResult.Ok)
            {
                var server = serverResult.Data;
                model.FirstName = server.FirstName;
                model.LastName = server.LastName;
                model.DoB = server.DoB;
                model.HireDate = server.HireDate;
                model.ServerID = server.ServerId;
                return View(model);
            }
            else
            {
                TempData["Message"] = $"Error getting server information : {serverResult.Message}.";
                return RedirectToAction("GetServers");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditServer(EditServerModel model)
        {
            var serverService = _serviceFactory.CreateServerService();

            if (ModelState.IsValid) 
            {
                var entity = new Server
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DoB = model.DoB,
                    HireDate = model.HireDate,
                    ServerId = model.ServerID,
                    TermDate = model.TermDate,
                };
                var serverResult = serverService.EditServer(entity);
                if (serverResult.Ok)
                {
                    TempData["Message"] = "Server has been edited succesfully!";
                    return RedirectToAction("GetServers");
                }
                else
                {
                    TempData["Message"] = $"Error editing server : {serverResult.Message}.";
                    return RedirectToAction("GetServers");
                }
            }
            return View(model);
        }
    }
}
