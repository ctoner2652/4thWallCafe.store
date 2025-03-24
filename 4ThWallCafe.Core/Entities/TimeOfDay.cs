using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class TimeOfDay
{
    public int TimeOfDayId { get; set; }

    public string TimeOfDayName { get; set; } = null!;

    public virtual List<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();
}
