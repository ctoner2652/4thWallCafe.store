using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface ITimeOfDayService
    {
        Result<List<TimeOfDay>> GetAllTimesOfDay();
        Result<TimeOfDay> GetTimeOfDayByID(int id);
    }
}
