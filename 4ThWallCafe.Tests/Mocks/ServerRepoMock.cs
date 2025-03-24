using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class ServerRepoMock : IServerRepository
    {
        private readonly List<Server> _servers;

        public ServerRepoMock()
        {
            _servers = new List<Server>
            {
                new Server { ServerId = 1, FirstName = "John", LastName = "Doe", DoB = new DateOnly(1990, 5, 20), HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-3)) },
                new Server { ServerId = 2, FirstName = "Jane", LastName = "Smith", DoB = new DateOnly(1995, 10, 15), HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2)) },
                new Server { ServerId = 3, FirstName = "Mike", LastName = "Brown", DoB = new DateOnly(1988, 3, 8), HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5)), TermDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2)) },
                new Server { ServerId = 4, FirstName = "Sara", LastName = "Williams", DoB = new DateOnly(2000, 7, 25), HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)) }
            };
        }

        public void AddServer(Server server)
        {
            server.ServerId = 5;
            _servers.Add(server);
        }

        public void EditServer(Server server)
        {
            var existing = _servers.FirstOrDefault(s => s.ServerId == server.ServerId);
            if (existing != null)
            {
                existing.FirstName = server.FirstName;
                existing.LastName = server.LastName;
                existing.DoB = server.DoB;
                existing.HireDate = server.HireDate;
                existing.TermDate = server.TermDate;
            }
        }

        public List<Server> GetAllServers()
        {
            return _servers.ToList();
        }

        public Server GetServer(int id)
        {
            return _servers.FirstOrDefault(s => s.ServerId == id);
        }
    }
}
