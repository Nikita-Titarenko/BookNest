using System;
using System.Collections.Generic;

namespace BookNest.Infrastructure;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string HotelName { get; set; } = null!;

    public string HotelDescription { get; set; } = null!;

    public string HotelCity { get; set; } = null!;

    public int AppUserId { get; set; }

    public virtual AppUser AppUser { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
