using System;
using System.Collections.Generic;

namespace WishlistService.Models;

public partial class Notification
{
    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Usertable User { get; set; } = null!;
}
