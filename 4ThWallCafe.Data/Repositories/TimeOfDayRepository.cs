using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class TimeOfDayRepository : ITimeOfDayRepository
    {
        private FourthWallCafeContext _dbContext;

        public TimeOfDayRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<TimeOfDay> GetAllTimesOfDay()
        {
            return _dbContext.TimeOfDay.ToList();
        }

        public TimeOfDay GetTimeOfDayByID(int id)
        {
            return _dbContext.TimeOfDay.FirstOrDefault(t => t.TimeOfDayId == id);
        }
    }
}
