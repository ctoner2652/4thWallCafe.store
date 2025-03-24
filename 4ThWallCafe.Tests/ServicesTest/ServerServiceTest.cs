using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Application.Services;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.Tests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace _4ThWallCafe.Tests.ServicesTest
{
    [TestFixture]
    internal class ServerServiceTest
    {
        private IServerService _serverService;

        [SetUp]
        public void Setup()
        {
            _serverService = new ServerService(new ServerRepoMock(), NullLogger<ServerService>.Instance);
        }

        [Test]
        public void GetAllServers_ShouldReturnListOfServers()
        {
            var result = _serverService.GetAllServers();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetServerById_ValidId_ShouldReturnServer()
        {
            int validServerId = 1;

            var result = _serverService.GetServer(validServerId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
        }

        [Test]
        public void GetServerById_InvalidId_ShouldReturnNull()
        {
            int invalidServerId = 999;

            var result = _serverService.GetServer(invalidServerId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }
    }
}
