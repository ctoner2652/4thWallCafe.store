using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.Extensions.Logging;

namespace _4ThWallCafe.Application.Services
{
    public class ServerService : IServerService
    {
        private readonly IServerRepository _serverRepository;
        private readonly ILogger _logger;
        public ServerService(IServerRepository serverRepository, ILogger<ServerService> logger)
        {
            _serverRepository = serverRepository;
            _logger = logger;
        }

        public Result AddServer(Server server)
        {
            try
            {
                _serverRepository.AddServer(server);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditServer(Server server)
        {
            try
            {
                _serverRepository.EditServer(server);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Server>> GetAllServers()
        {
            try
            {
                var servers = _serverRepository.GetAllServers();
                if(servers.Count >= 1)
                {
                    return ResultFactory.Success(servers);
                }
                else
                {
                    return ResultFactory.Fail<List<Server>>("Error getting all Servers");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<Server>>(ex.Message);
            }
        }

        public Result<Server> GetServer(int id)
        {
            try
            {
                var server = _serverRepository.GetServer(id);
                return server is null ? ResultFactory.Fail<Server>($"No Server found for user with ID : {id}") :
    ResultFactory.Success(server);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<Server>(ex.Message);
            }
        }
    }
}
