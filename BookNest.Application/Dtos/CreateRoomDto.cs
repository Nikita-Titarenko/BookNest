namespace BookNest.Application.Dtos
{
    public class CreateRoomDto
    {
        public string RoomName { get; set; } = string.Empty;

        public int RoomPrice { get; set; }

        public int RoomQuantity { get; set; }

        public int GuestsNumber { get; set; }

        public decimal RoomSize { get; set; }

        public int HotelId { get; set; }
    }
}
