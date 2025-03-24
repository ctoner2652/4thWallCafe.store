using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Repositories
{
    public interface ITimeOfDayRepository
    {
        List<TimeOfDay> GetAllTimesOfDay();
        TimeOfDay GetTimeOfDayByID(int id);
    }
}
