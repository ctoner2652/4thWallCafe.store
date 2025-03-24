using System;
using System.Collections.Generic;

namespace _4ThWallCafe.MVC.Core.Entities;

public partial class Server
{
    public int ServerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public DateOnly? TermDate { get; set; }

    public DateOnly DoB { get; set; }

    public virtual List<CafeOrder> CafeOrders { get; set; } = new List<CafeOrder>();
}
