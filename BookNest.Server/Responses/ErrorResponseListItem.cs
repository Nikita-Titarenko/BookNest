namespace BookNest.Server.Responses
{
    public class ErrorResponseListItem
    {
        public int? Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? Field { get; set; }
    }
}
