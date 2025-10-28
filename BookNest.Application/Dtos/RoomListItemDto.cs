namespace BookNest.Application.Dtos
{
    public class RoomListItemDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int RoomPrice { get; set; }
        public decimal RoomSize { get; set; }
        public int RoomQuantity { get; set; }
        public int GuestsNumber { get; set; }
    }
}
