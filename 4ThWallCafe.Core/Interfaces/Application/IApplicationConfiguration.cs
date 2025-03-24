using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4ThWallCafe.Core.Interfaces.Application
{
    public interface IApplicationConfiguration
    {
        string GetConnectionString();
        string GetMVCAPIUserName();
        string GetMVCAPIPassword();
        string GetAPIKey();
        string GetAPIAudience();
        string GetAPIIssuer();
    }
}
