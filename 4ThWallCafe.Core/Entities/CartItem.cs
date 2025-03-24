using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int ItemId { get; set; }

    public Guid UserSessionId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public int ItemPriceId { get; set; }

    public virtual Item Item { get; set; } = null!;
}
