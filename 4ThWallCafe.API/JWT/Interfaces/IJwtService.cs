using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace _4ThWallCafe.API.JWT.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(IdentityUser user, IList<Claim> userClaims);
    }
}
