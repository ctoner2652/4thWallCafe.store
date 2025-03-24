using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Application.Services;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.Tests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace _4ThWallCafe.Tests.ServicesTest
{
    internal class PaymentTypeServiceTest
    {
        private IPaymentTypeService _paymentTypeService;

        [SetUp]
        public void Setup()
        {
            _paymentTypeService = new PaymentTypeService(new PaymenTypeRepoMock(), NullLogger<PaymentTypeService>.Instance);
        }

        [Test]
        public void GetAllPaymentTypes_ShouldReturnListOfPaymentTypes()
        {
            var result = _paymentTypeService.GetAllPaymentTypes();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetPaymentTypeById_ValidId_ShouldReturnPaymentType()
        {
            int validPaymentTypeId = 1;

            var result = _paymentTypeService.GetPaymentTypeByID(validPaymentTypeId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
        }

        [Test]
        public void GetPaymentTypeById_InvalidId_ShouldReturnNull()
        {
            int invalidPaymentTypeId = 999;

            var result = _paymentTypeService.GetPaymentTypeByID(invalidPaymentTypeId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }
    }
}
