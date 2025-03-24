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
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly ILogger _logger;
        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository, ILogger<PaymentTypeService> logger)
        {
            _paymentTypeRepository = paymentTypeRepository;
            _logger = logger;
        }

        public Result<List<PaymentType>> GetAllPaymentTypes()
        {
            try
            {
                var types = _paymentTypeRepository.GetAllPaymentTypes();

                if (types.Count >= 1)
                {
                    return ResultFactory.Success(types);
                }
                else
                {
                    return ResultFactory.Fail<List<PaymentType>>("Error Getting Payment Types");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<List<PaymentType>>(ex.Message);
            }
        }

        public Result<PaymentType> GetPaymentTypeByID(int id)
        {
            try
            {
                var type = _paymentTypeRepository.GetPaymentTypeByID(id);
                return type is null ? ResultFactory.Fail<PaymentType>($"No Payment Type Found with ID : {id}") :
    ResultFactory.Success(type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultFactory.Fail<PaymentType>(ex.Message);
            }
        }
    }
}
