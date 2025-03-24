﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.Core.Interfaces.Services
{
    public interface IPaymentTypeService
    {
        Result<List<PaymentType>> GetAllPaymentTypes();
        Result<PaymentType> GetPaymentTypeByID(int id);
    }
}
