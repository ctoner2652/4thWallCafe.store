using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ItemPriceId { get; set; }

    public byte Quantity { get; set; }

    public decimal ExtendedPrice { get; set; }

    public virtual ItemPrice ItemPrice { get; set; } = null!;

    public virtual CafeOrder Order { get; set; } = null!;
}
