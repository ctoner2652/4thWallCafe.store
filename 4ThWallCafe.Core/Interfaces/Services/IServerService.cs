using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface IServerService
    {
        Result<Server> GetServer(int id);
        Result<List<Server>> GetAllServers();
        Result EditServer(Server server);
        Result AddServer(Server server);
    }
}
