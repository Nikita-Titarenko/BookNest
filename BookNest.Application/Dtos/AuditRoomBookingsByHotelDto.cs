using System.ComponentModel.DataAnnotations.Schema;

namespace BookNest.Application.Dtos
{
    public class AuditRoomBookingsByHotelDto
    {
        public int AuditAppUserRoomId { get; set; }
        public DateOnly? OldStartDate { get; set; }
        public DateOnly? NewStartDate { get; set; }
        public DateOnly? OldEndDate { get; set; }
        public DateOnly? NewEndDate { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public string AppUserEmail { get; set; } = string.Empty;
        public string AppUserFullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
