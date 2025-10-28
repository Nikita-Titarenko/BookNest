namespace BookNest.Application.Dtos
{
    public class RoomBookingByUserDto
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
