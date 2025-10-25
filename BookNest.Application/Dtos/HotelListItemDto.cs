namespace BookNest.Application.Dtos
{
    public class HotelListItemDto
    {
        public int hotel_id { get; set; }
        public string hotel_name { get; set; } = string.Empty;
        public string hotel_city { get; set; } = string.Empty;
        public int total_room_count { get; set; }
    }
}
