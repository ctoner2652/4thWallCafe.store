using System.Net.Http;
using System.Text;
using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Models;
using _4ThWallCafe.MVC.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NuGet.Common;

namespace _4ThWallCafe.MVC.Controllers
{
    public class APIManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public APIManagementController(UserManager<IdentityUser> userManager, IApiClientFactory apiClientFactory, Microsoft.Extensions.Logging.ILogger<APIManagementController> logger)
        {
            _userManager = userManager;
            _apiClientFactory = apiClientFactory;
            _logger = logger;
        }
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("API"))
                {
                    model.Add(new UserViewModel
                    {
                        UserName = user.UserName,
                    });
                }
                
            }

            return View(model);

        }
        [HttpGet]
        public IActionResult CreateApiUser()
        {
            var model = new CreateNewAPIUser();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApiUser(CreateNewAPIUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser { UserName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "API"); 
                var apiManagementClient = await _apiClientFactory.CreateAPIUserManagement();

                var token = await apiManagementClient.GenerateToken(model.UserName, model.Password);
                TempData["Token"] = $"{token}";
                return RedirectToAction("GetUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditApiUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                TempData["Message"] = "Unable to find user";
                _logger.LogWarning("Unable to find user");
                return RedirectToAction("GetUsers");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                UserName = user.UserName
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditApiUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                TempData["Message"] = "Unable to find user";
                _logger.LogWarning("Unable to find user");
                return RedirectToAction("GetUsers");
            }
            TempData["Success"] = "User roles updated successfully!";
            return RedirectToAction("GetUsers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                TempData["Message"] = "Unable to find user";
                _logger.LogWarning("Unable to find user");
                return RedirectToAction("GetUsers");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Message"] = "Failed to delete user.";
                _logger.LogWarning("Unable to delete user");
                return RedirectToAction("GetUsers");
            }

            TempData["Message"] = "User deleted successfully!";
            return RedirectToAction("GetUsers");
        }
    }
}
