using System;
using System.Collections.Generic;

namespace CaseStudyPCM2.Models;

public partial class Usertable
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
