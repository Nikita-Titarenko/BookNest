namespace BookNest.Application.Dtos
{
    public class CreateRoomDto
    {
        public string RoomName { get; set; } = string.Empty;

        public string RoomDescription { get; set; } = string.Empty;

        public int RoomPrice { get; set; }

        public int RoomQuantity { get; set; }

        public int GuestsNumber { get; set; }

        public int RoomSize { get; set; }

        public int HotelId { get; set; }
    }
}
