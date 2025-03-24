using _4ThWallCafe.MVC.Models;
using _4ThWallCafe.MVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.MVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                model.Add(new UserViewModel
                {
                    UserName = user.UserName,
                    IsManager = roles.Contains("Manager"),
                    IsAccountant = roles.Contains("Accountant")
                });
            }

            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                TempData["Message"] = "Unable to find user";
                return RedirectToAction("GetUsers");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                UserName = user.UserName,
                IsManager = roles.Contains("Manager"),
                IsAccountant = roles.Contains("Accountant")
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                TempData["Message"] = "Unable to find user";
                return RedirectToAction("GetUsers");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (model.IsManager && !roles.Contains("Manager"))
                await _userManager.AddToRoleAsync(user, "Manager");
            else if (!model.IsManager && roles.Contains("Manager"))
                await _userManager.RemoveFromRoleAsync(user, "Manager");

            if (model.IsAccountant && !roles.Contains("Accountant"))
                await _userManager.AddToRoleAsync(user, "Accountant");
            else if (!model.IsAccountant && roles.Contains("Accountant"))
                await _userManager.RemoveFromRoleAsync(user, "Accountant");

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
                return RedirectToAction("GetUsers");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Message"] = "Failed to delete user.";
                return RedirectToAction("GetUsers");
            }

            TempData["Message"] = "User deleted successfully!";
            return RedirectToAction("GetUsers");
        }
    }
}
