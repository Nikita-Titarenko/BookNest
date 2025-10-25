using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IUserService
    {
        Task<Result<int>> Login(LoginDto dto);
        Task<Result<int>> Register(RegisterDto dto);
    }
}