using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Interfaces.Repositories;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Tests.Mocks
{
    internal class PaymenTypeRepoMock : IPaymentTypeRepository
    {
        private readonly List<PaymentType> _paymentTypes;

        public PaymenTypeRepoMock()
        {
            _paymentTypes = new List<PaymentType>
            {
                new PaymentType { PaymentTypeId = 1, PaymentTypeName = "Credit Card" },
                new PaymentType { PaymentTypeId = 2, PaymentTypeName = "Cash" },
                new PaymentType { PaymentTypeId = 3, PaymentTypeName = "Gift Card" },
                new PaymentType { PaymentTypeId = 4, PaymentTypeName = "Online Payment" }
            };
        }

        public List<PaymentType> GetAllPaymentTypes()
        {
            return _paymentTypes.ToList();
        }

        public PaymentType GetPaymentTypeByID(int id)
        {
            return _paymentTypes.FirstOrDefault(pt => pt.PaymentTypeId == id);
        }
    }
}
