namespace BookNest.Application.Dtos
{
    public class RoomBookingByHotelDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string AppUserEmail { get; set; } = string.Empty;
        public string AppUserFullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int BookingsTotalCount { get; set; }
    }
}