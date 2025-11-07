using BookNest.Application.Dtos.AppUsers;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IUserService
    {
        Task<Result<AppUserDto>> GetAppUserAsyncAsync(int userId);
        Task<Result<int>> LoginAsync(LoginDto dto);
        Task<Result<AppUserDto>> RegisterAsync(RegisterDto dto);
    }
}