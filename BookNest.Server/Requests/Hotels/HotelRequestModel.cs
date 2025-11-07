using BookNest.Server.Attributes;

namespace BookNest.Server.Requests.Hotels
{
    public class HotelRequestModel
    {
        [StringLengthWithCode(100, 50027, MinimumLength = 3, ErrorMessage = "Hotel name length must be from 3 to 100")]
        public string HotelName { get; set; } = string.Empty;
        [StringLengthWithCode(200, 50028, MinimumLength = 3, ErrorMessage = "Hotel city must be from 3 to 200")]
        public string HotelCity { get; set; } = string.Empty;
        public string HotelDescription { get; set; } = string.Empty;
    }
}
