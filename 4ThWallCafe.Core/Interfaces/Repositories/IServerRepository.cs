using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface IServerRepository
    {
        Server GetServer(int id);
        List<Server> GetAllServers();
        void EditServer(Server server);
        void AddServer(Server server);
    }
}
