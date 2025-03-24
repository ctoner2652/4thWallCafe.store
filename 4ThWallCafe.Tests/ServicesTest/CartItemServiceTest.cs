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
    internal class CartItemServiceTest
    {
        private ICartItemService _cartItemService;

        [SetUp]
        public void Setup()
        {
            _cartItemService = new CartItemService(new CartItemRepoMock(), NullLogger<CartItemService>.Instance);
        }

        [Test]
        public void GetUsersCart_ValidSessionId_ShouldReturnCartItems()
        {
            var validSessionId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var result = _cartItemService.GetUsersCart(validSessionId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetUsersCart_InvalidSessionId_ShouldReturnEmpty()
        {
            var invalidSessionId = Guid.NewGuid();

            var result = _cartItemService.GetUsersCart(invalidSessionId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True); 
            Assert.That(result.Data, Is.Empty);
        }

        
    }
}
