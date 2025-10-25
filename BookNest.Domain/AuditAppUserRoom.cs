using System;
using System.Collections.Generic;

namespace BookNest.Infrastructure;

public partial class AuditAppUserRoom
{
    public int AuditAppUserRoomId { get; set; }

    public int? AppUserId { get; set; }

    public int? RoomId { get; set; }

    public string? ActionType { get; set; }

    public DateTime? ActionDateTime { get; set; }

    public DateOnly? OldStartDate { get; set; }

    public DateOnly? NewStartDate { get; set; }

    public DateOnly? OldEndDate { get; set; }

    public DateOnly? NewEndDate { get; set; }
}
