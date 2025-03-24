using _4ThWallCafe.API.JWT.Interfaces;
using _4ThWallCafe.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _4ThWallCafe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IJwtService jwtService, UserManager<IdentityUser> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var token = _jwtService.GenerateToken(user, userClaims);

            return Ok(new { Token =  token });
        }
    }
}
