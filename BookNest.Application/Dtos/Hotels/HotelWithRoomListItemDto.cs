namespace BookNest.Application.Dtos.Hotels
{
    public class HotelWithRoomListItemDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string HotelCity { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int RoomPrice { get; set; }
    }
}
