using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace caselibrary.Models;

public partial class Usertable
{
    
    public int? UserId { get; set; }
    [Required]
    public string Firstname { get; set; } 

    public string? Lastname { get; set; } 

    public string ?Email { get; set; } 

    [Required]
    public string upassword { get; set; }

    public int ?phonenumber { get; set; }
    public virtual ICollection<Address> ?Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Cart>? Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Notification>? Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Ordertable>? Ordertables { get; set; } = new List<Ordertable>();

    public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();

    
}
