using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class TimeOfDayRepoMock : ITimeOfDayRepository
    {
        private readonly List<TimeOfDay> _timeOfDays;

        public TimeOfDayRepoMock()
        {
            _timeOfDays = new List<TimeOfDay>
            {
                new TimeOfDay { TimeOfDayId = 1, TimeOfDayName = "Breakfast" },
                new TimeOfDay { TimeOfDayId = 2, TimeOfDayName = "Lunch" },
                new TimeOfDay { TimeOfDayId = 3, TimeOfDayName = "Dinner" },
                new TimeOfDay { TimeOfDayId = 4, TimeOfDayName = "Late Night" }
            };
        }

        public List<TimeOfDay> GetAllTimesOfDay()
        {
            return _timeOfDays.ToList();
        }

        public TimeOfDay GetTimeOfDayByID(int id)
        {
            return _timeOfDays.FirstOrDefault(t => t.TimeOfDayId == id);
        }
    }
}
