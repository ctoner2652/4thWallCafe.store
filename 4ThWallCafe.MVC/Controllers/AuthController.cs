using _4ThWallCafe.MVC.Models;
using _4ThWallCafe.MVC.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
            , ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new CreateNewUser();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateNewUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.IsManager != false)
                    {
                        await _userManager.AddToRoleAsync(user, "Manager");
                    } 
                    if(model.IsAccountant != false)
                    {
                        await _userManager.AddToRoleAsync(user, "Accountant");
                    }
                    TempData["Message"] = $"{user.UserName}'s account has been created!";
                    _logger.LogInformation("User account has been created succesfully");
                    return RedirectToAction("Index", "Manager");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new UserSignIn();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserSignIn model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Username, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    TempData["Message"] = $"Welcome {model.Username}!";
                    _logger.LogInformation("User account has been logged in succesfully");
                    return RedirectToAction("Index", "Manager");
                }
                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = "You have been signed out!";
            _logger.LogInformation("User account has been signed out succesfully");
            return RedirectToAction("Index", "Home");
        }
    }
}
