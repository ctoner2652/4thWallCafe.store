using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class PaymentType
{
    public int PaymentTypeId { get; set; }

    public string PaymentTypeName { get; set; } = null!;

    public virtual List<CafeOrder> CafeOrders { get; set; } = new List<CafeOrder>();
}
