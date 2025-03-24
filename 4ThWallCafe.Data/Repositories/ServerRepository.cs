using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class ServerRepository : IServerRepository
    {
        private FourthWallCafeContext _dbContext;

        public ServerRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddServer(Server server)
        {
            _dbContext.Server.Add(server);
            _dbContext.SaveChanges();
        }

        public void EditServer(Server server)
        {
            _dbContext.Server.Update(server);
            _dbContext.SaveChanges();
        }

        public List<Server> GetAllServers()
        {
            return _dbContext.Server.ToList();
        }

        public Server GetServer(int id)
        {
            return _dbContext.Server.FirstOrDefault(s => s.ServerId == id)!;
        }
    }
}
