using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IUserService
    {
        Task<Result<AppUserDto>> GetAppUserAsync(int userId);
        Task<Result<int>> Login(LoginDto dto);
        Task<Result<AppUserDto>> Register(RegisterDto dto);
    }
}