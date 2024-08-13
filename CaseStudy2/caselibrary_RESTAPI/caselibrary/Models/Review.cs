using System;
using System.Collections.Generic;

namespace caselibrary.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Rating { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Usertable? User { get; set; }
}
