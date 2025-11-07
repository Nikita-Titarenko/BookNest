namespace BookNest.Application.Dtos.Hotels
{
    public class CreateHotelResultDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string HotelCity { get; set; } = string.Empty;
        public string HotelDescription { get; set; } = string.Empty;
        public int AppUserId { get; set; }
    }
}
