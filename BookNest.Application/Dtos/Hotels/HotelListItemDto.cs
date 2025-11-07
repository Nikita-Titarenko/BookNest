namespace BookNest.Application.Dtos.Hotels
{
    public class HotelListItemDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string HotelCity { get; set; } = string.Empty;
        public int TotalRoomCount { get; set; }
    }
}
