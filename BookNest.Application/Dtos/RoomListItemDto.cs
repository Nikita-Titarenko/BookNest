namespace BookNest.Application.Dtos
{
    public class RoomListItemDto
    {
        public int room_id { get; set; }

        public string room_name { get; set; } = string.Empty;

        public int room_price { get; set; }

        public int room_size { get; set; }

        public int room_quantity { get; set; }
    }
}
