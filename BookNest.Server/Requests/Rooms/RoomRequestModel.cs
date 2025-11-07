using BookNest.Server.Attributes;

namespace BookNest.Application.Dtos.Rooms
{
    public class RoomRequestModel
    {
        [StringLengthWithCode(200, 50029, MinimumLength = 3, ErrorMessage = "Room name length must be from 3 to 200")]
        public string RoomName { get; set; } = string.Empty;

        public int RoomPrice { get; set; }

        public int RoomQuantity { get; set; }

        public int GuestsNumber { get; set; }

        public decimal RoomSize { get; set; }
    }
}
