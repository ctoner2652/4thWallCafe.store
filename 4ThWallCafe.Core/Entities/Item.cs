using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class Item
{
    public int ItemId { get; set; }

    public int CategoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual List<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();
}
