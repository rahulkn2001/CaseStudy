using System;
using System.Collections.Generic;

namespace EmailVErification.Model;

public partial class Usertable
{
    public int UserId { get; set; }

    public string ?Firstname { get; set; } = null!;

    public string ?Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Upassword { get; set; }

    public bool IsEmailVerified { get; set; }

    public string? VerificationToken { get; set; }

    public virtual ICollection<Address> ?Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Cart> ?Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Notification>? Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Ordertable>? Ordertables { get; set; } = new List<Ordertable>();

    public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();

    public virtual Wishlist? Wishlist { get; set; }
}
