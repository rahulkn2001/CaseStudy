using System;
using System.Collections.Generic;

namespace ProductMicroservices.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public int? OrderId { get; set; }

    public DateTime ShipmentDate { get; set; }

    public string? TrackingNumber { get; set; }

    public virtual Ordertable? Order { get; set; }
}
