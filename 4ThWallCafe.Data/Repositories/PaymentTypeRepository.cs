using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Data.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private FourthWallCafeContext _dbContext;

        public PaymentTypeRepository(FourthWallCafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PaymentType> GetAllPaymentTypes()
        {
            return _dbContext.PaymentType.ToList();
        }

        public PaymentType GetPaymentTypeByID(int id)
        {
            return _dbContext.PaymentType.FirstOrDefault(pt => pt.PaymentTypeId == id)!;
        }
    }
}
