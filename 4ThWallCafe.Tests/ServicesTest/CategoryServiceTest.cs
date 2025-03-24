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
using Microsoft.Testing.Platform.Logging;
using Moq;
using NUnit.Framework;

namespace _4ThWallCafe.Tests.ServicesTest
{
    [TestFixture]
    public class CategoryServiceTest
    {
        private ICategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            _categoryService = new CategoryService(new CategoryRepoMock(), NullLogger<CategoryService>.Instance);
        }

        [Test]
        public void GetAllCategories_ShouldReturnListOfCategories()
        {
            var result = _categoryService.GetAllCategories();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }
        [Test]
        public void GetCategory_ValidId_ShouldReturnCategory()
        {
            var validCategoryId = 1;

            var result = _categoryService.GetCategory(validCategoryId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.CategoryId, Is.EqualTo(validCategoryId));
        }

        [Test]
        public void GetCategory_InvalidId_ShouldReturnError()
        {
            var invalidCategoryId = 9999;

            var result = _categoryService.GetCategory(invalidCategoryId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }

        
    }
}
