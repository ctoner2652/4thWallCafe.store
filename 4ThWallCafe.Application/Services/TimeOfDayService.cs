using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.Core.Interfaces.Services;
using _4ThWallCafe.MVC.Core.Entities;
using Microsoft.Extensions.Logging;

namespace _4ThWallCafe.Application.Services
{
    public class TimeOfDayService : ITimeOfDayService
    {
        private ITimeOfDayRepository _timeOfDayRepository;
        private ILogger _logger;
        public TimeOfDayService(ITimeOfDayRepository timeOfDayRepository, ILogger<TimeOfDayService> logger)
        {
            _timeOfDayRepository = timeOfDayRepository;
            _logger = logger;
        }

        public Result<List<TimeOfDay>> GetAllTimesOfDay()
        {
            try
            {
                var times = _timeOfDayRepository.GetAllTimesOfDay();
                return ResultFactory.Success(times);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<TimeOfDay>>(ex.Message);
            }
        }

        public Result<TimeOfDay> GetTimeOfDayByID(int id)
        {
            try
            {
                var time = _timeOfDayRepository.GetTimeOfDayByID(id);
                return time is null ? ResultFactory.Fail<TimeOfDay>($"TimeOfDay with ID : {id} not found. ") :
                    ResultFactory.Success(time);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<TimeOfDay>(ex.Message);
            }
        }
    }
}
