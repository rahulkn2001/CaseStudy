using System;
using System.Collections.Generic;

namespace ProductMicroservices.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual Ordertable? Order { get; set; }
}
