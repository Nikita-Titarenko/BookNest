using BookNest.Server.Attributes;

namespace BookNest.Server.Requests.AppUsers
{
    public class LoginRequestModel
    {
        [StringLengthWithCode(254, 50024, MinimumLength = 3, ErrorMessage = "Email length must be from 3 to 254")]
        public string Email { get; set; } = string.Empty;
        [StringLengthWithCode(30, 50025, MinimumLength = 6, ErrorMessage = "Password length must be from 6 to 30")]
        public string Password { get; set; } = string.Empty;
    }
}
