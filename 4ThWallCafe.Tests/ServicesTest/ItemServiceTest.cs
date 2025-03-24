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
    internal class ItemServiceTest
    {
        private IItemService _itemService;

        [SetUp]
        public void Setup()
        {
            _itemService = new ItemService(new ItemRepoMock(), NullLogger<ItemService>.Instance);
        }

        [Test]
        public void GetAllItems_ShouldReturnItems()
        {
            var result = _itemService.GetAllItems();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetItem_ValidId_ShouldReturnItem()
        {
            int validId = 101;

            var result = _itemService.GetItem(validId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.ItemId, Is.EqualTo(validId));
        }

        [Test]
        public void GetItem_InvalidId_ShouldReturnNull()
        {
            int invalidId = 999;

            var result = _itemService.GetItem(invalidId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public void GetItemsByCategory_ValidCategory_ShouldReturnItems()
        {
            int categoryId = 1;

            var result = _itemService.GetItemsByCategory(categoryId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetItemsByCategory_InvalidCategory_ShouldReturnEmptyList()
        {
            int invalidCategoryId = 999;

            var result = _itemService.GetItemsByCategory(invalidCategoryId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }

        
    }
}
