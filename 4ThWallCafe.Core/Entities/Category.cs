using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual List<Item> Items { get; set; } = new List<Item>();
}
