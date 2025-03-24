using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class ItemPrice
{
    public int ItemPriceId { get; set; }

    public int ItemId { get; set; }

    public int TimeOfDayId { get; set; }

    public decimal Price { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual TimeOfDay TimeOfDay { get; set; } = null!;
}
