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
    internal class OrderItemServiceTest
    {
        private IOrderItemService _orderItemService;

        [SetUp]
        public void Setup()
        {
            _orderItemService = new OrderItemService(new OrderItemRepoMock(), NullLogger<OrderItemService>.Instance);
        }

        [Test]
        public void GetAllOrderItems_ShouldReturnOrderItems()
        {
            var result = _orderItemService.GetAllOrderItems();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetOrderItem_ValidId_ShouldReturnOrderItem()
        {
            int validId = 1;

            var result = _orderItemService.GetOrderItem(validId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.OrderItemId, Is.EqualTo(validId));
        }

        [Test]
        public void GetOrderItem_InvalidId_ShouldReturnNull()
        {
            int invalidId = 999;

            var result = _orderItemService.GetOrderItem(invalidId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }

        

        
    }
}
