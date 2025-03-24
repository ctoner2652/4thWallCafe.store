using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface ITimeOfDayAPIClient
    {
        Task<TimeOfDay> GetTimeOfDayByID(int timeOfDayID);
        Task<List<TimeOfDay>> GetAllTimesOfDay();
    }
}
