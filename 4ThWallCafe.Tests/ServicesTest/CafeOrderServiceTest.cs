using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Application.Services;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using _4ThWallCafe.Tests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace _4ThWallCafe.Tests.ServicesTest
{
    [TestFixture]
    internal class CafeOrderServiceTest 
    {
        private ICafeOrderService _cafeOrderService;

        [SetUp]
        public void Setup()
        {
            _cafeOrderService = new CafeOrderService(new CafeOrderRepoMock(), NullLogger<CafeOrderService>.Instance);
        }

        [Test]
        public void GetAllCafeOrders_ShouldReturnListOfOrders()
        {
          
            var result = _cafeOrderService.GetAllCafeOrders();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetCafeOrder_ValidId_ShouldReturnOrder()
        {
            var validOrderId = 1;

            var result = _cafeOrderService.GetCafeOrder(validOrderId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.OrderId, Is.EqualTo(validOrderId));
        }

        [Test]
        public void GetCafeOrder_InvalidId_ShouldReturnError()
        {
            var invalidOrderId = 9999;

            var result = _cafeOrderService.GetCafeOrder(invalidOrderId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }

    }
}
