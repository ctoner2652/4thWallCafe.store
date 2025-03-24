using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _4ThWallCafe.API.JWT.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace _4ThWallCafe.API.JWT.Implementations
{
    public class JwtTokenService : IJwtService
    {
        private readonly AppConfiguration _configuration;

        public JwtTokenService(AppConfiguration configuration)
        {
            _configuration = configuration;
        }    

        public string GenerateToken(IdentityUser user, IList<Claim> userClaims)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            claims.AddRange(claims);

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetAPIKey()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetAPIIssuer(),
                audience: _configuration.GetAPIAudience(),
                claims: claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
