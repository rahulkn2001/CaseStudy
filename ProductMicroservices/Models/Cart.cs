using System;
using System.Collections.Generic;

namespace ProductMicroservices.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Usertable? User { get; set; }
}
