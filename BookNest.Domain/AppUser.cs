using System;
using System.Collections.Generic;

namespace BookNest.Infrastructure;

public partial class AppUser
{
    public int AppUserId { get; set; }

    public string AppUserFullname { get; set; } = null!;

    public string AppUserEmail { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<AppUserRoom> AppUserRooms { get; set; } = new List<AppUserRoom>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
