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
    internal class TimeOfDayServiceTest
    {
        private ITimeOfDayService _timeOfDayService;

        [SetUp]
        public void Setup()
        {
            _timeOfDayService = new TimeOfDayService(new TimeOfDayRepoMock(), NullLogger<TimeOfDayService>.Instance);
        }

        [Test]
        public void GetAllTimesOfDay_ShouldReturnListOfTimes()
        {
            var result = _timeOfDayService.GetAllTimesOfDay();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Empty);
        }

        [Test]
        public void GetTimeOfDayById_ValidId_ShouldReturnTimeOfDay()
        {
            int validTimeOfDayId = 1;

            var result = _timeOfDayService.GetTimeOfDayByID(validTimeOfDayId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.Not.Null);
        }

        [Test]
        public void GetTimeOfDayById_InvalidId_ShouldReturnNull()
        {
            int invalidTimeOfDayId = 999;

            var result = _timeOfDayService.GetTimeOfDayByID(invalidTimeOfDayId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ok, Is.False);
            Assert.That(result.Data, Is.Null);
        }
    }
}
