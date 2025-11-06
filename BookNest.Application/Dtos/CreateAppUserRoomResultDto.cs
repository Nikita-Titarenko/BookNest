namespace BookNest.Application.Dtos
{
    public class CreateAppUserRoomResultDto
    {
        public int UserId { get; set; }

        public int RoomId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
