using BookNest.Server.Attributes;

namespace BookNest.Server.Requests.AppUsers
{
    public class RegisterRequestModel
    {
        [StringLengthWithCode(150, 50023, MinimumLength = 3, ErrorMessage = "Fullname length must be from 3 to 150")]
        public string Fullname { get; set; } = string.Empty;
        [StringLengthWithCode(254, 50024, MinimumLength = 3, ErrorMessage = "Email length must be from 3 to 254")]
        public string Email { get; set; } = string.Empty;
        [StringLengthWithCode(30, 50025, MinimumLength = 6, ErrorMessage = "Password length must be from 6 to 30")]
        public string Password { get; set; } = string.Empty;
        [StringLengthWithCode(20, 50026, MinimumLength = 9, ErrorMessage = "Phone length must be from 9 to 20")]
        public string Phone { get; set; } = string.Empty;
    }
}