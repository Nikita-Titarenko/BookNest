namespace BookNest.Application.Dtos
{
    public class HotelWithRoomListItemDto
    {
        public int hotel_id { get; set; }
        public string hotel_name { get; set; } = string.Empty;
        public string hotel_city { get; set; } = string.Empty;
        public int room_id { get; set; }
        public string room_name { get; set; } = string.Empty;
        public int room_price { get; set; }
    }
}
