using System;
using System.Collections.Generic;

namespace CaseStudyWishlist.Models;

public partial class Ordertable
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment? Payment { get; set; }

    public virtual Shipment? Shipment { get; set; }

    public virtual Usertable? User { get; set; }
}
