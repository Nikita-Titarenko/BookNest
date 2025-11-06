namespace BookNest.Application.Dtos
{
    public class AppUserDto
    {
        public int AppUserId { get; set; }

        public string AppUserFullname { get; set; } = string.Empty;

        public string AppUserEmail { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
