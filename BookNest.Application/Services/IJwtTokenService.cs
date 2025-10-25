namespace BookNest.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(int userId);
    }
}