using System;
using System.Collections.Generic;

namespace ReviewService.Models;

public partial class Address
{
    public int UserId { get; set; }

    public int HouseNumber { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public virtual Usertable User { get; set; } = null!;
}
