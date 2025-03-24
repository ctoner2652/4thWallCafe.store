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
    internal class ItemPriceServiceTest
    {

        private IItemPriceService _itemPriceService;

        [SetUp]
        public void Setup()
        {
            _itemPriceService = new ItemPriceService(new ItemPriceRepoMock(), NullLogger<ItemPriceService>.Instance);
        }

        [Test]
        public void GetAllItemPrices_ShouldReturnItemPrices()
        {
            var result = _itemPriceService.GetAllItemPrices();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetAllActiveItemPrices_ShouldReturnActiveItemPrices()
        {
            var result = _itemPriceService.GetAllActiveItemPrices();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetItemPrice_ValidId_ShouldReturnItemPrice()
        {
            int validId = 1;

            var result = _itemPriceService.GetItemPrice(validId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.ItemPriceId, Is.EqualTo(validId));
        }

        [Test]
        public void GetItemPrice_InvalidId_ShouldReturnNull()
        {
            int invalidId = 999;

            var result = _itemPriceService.GetItemPrice(invalidId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }

    }
}

