using System;
using System.Collections.Generic;

namespace EmailVErification.Model;

public partial class Address
{
    public int UserId { get; set; }

    public int HouseNumber { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int AddressId { get; set; }

    public virtual Usertable User { get; set; } = null!;
}
