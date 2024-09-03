﻿using System;
using System.Collections.Generic;

namespace EmailVErification.Model;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime? RestockDate { get; set; }

    public virtual Product? Product { get; set; }
}
