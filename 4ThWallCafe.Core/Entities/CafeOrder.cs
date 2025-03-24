using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class CafeOrder
{
    public int OrderId { get; set; }

    public int? ServerId { get; set; }

    public int PaymentTypeId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Tip { get; set; }

    public decimal? AmountDue { get; set; }

    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual Server? Server { get; set; }
}
