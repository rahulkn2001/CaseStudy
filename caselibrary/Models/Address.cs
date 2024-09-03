﻿using System;
using System.Collections.Generic;

namespace caselibrary.Models;

public partial class Address
{
    public int UserId { get; set; }

    public int HouseNumber { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string ?Addressline1 { get; set; }

    public string? Addressline2 { get; set; }

    public int ?zipcode{ get; set; }

    public virtual Usertable User { get; set; } = null!;
}
