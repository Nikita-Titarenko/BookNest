using System;
using System.Collections.Generic;

namespace BookNest.Infrastructure;

public partial class AppUserRoom
{
    public int AppUserId { get; set; }

    public int RoomId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual AppUser AppUser { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
