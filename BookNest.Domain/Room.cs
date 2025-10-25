using System;
using System.Collections.Generic;

namespace BookNest.Infrastructure;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public string? RoomDescription { get; set; }

    public int RoomPrice { get; set; }

    public int RoomQuantity { get; set; }

    public int GuestsNumber { get; set; }

    public decimal? RoomSize { get; set; }

    public int? HotelId { get; set; }

    public virtual ICollection<AppUserRoom> AppUserRooms { get; set; } = new List<AppUserRoom>();

    public virtual Hotel? Hotel { get; set; }
}
