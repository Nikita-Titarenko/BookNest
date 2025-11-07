namespace BookNest.Application.Dtos.AppUserRooms
{
    public class BookingRequestModel
    {
        public int RoomId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
