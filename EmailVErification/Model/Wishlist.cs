using System;
using System.Collections.Generic;

namespace EmailVErification.Model;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int? UserId { get; set; }

    public virtual Usertable? User { get; set; }

    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
