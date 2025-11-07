using System;
using System.Data;
using BookNest.Application.Dtos.AppUsers;
using BookNest.Application.Services;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IExecuteSafe _executeSafe;

        public UserService(ApplicationDbContext context, IExecuteSafe executeSafe)
        {
            _context = context;
            _executeSafe = executeSafe;
        }

        public async Task<Result<AppUserDto>> RegisterAsync(RegisterDto dto)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var result = await _context.AppUserDtos
                    .FromSqlRaw(
                        "EXEC dbo.Register @Fullname, @Email, @Phone, @Password",
                        new SqlParameter("@Fullname", dto.Fullname),
                        new SqlParameter("@Email", dto.Email),
                        new SqlParameter("@Phone", dto.Phone),
                        new SqlParameter("@Password", dto.Password)
                    )
                    .ToListAsync();

                return Result.Ok(result.First());
            });
        }

        public async Task<Result<int>> LoginAsync(LoginDto dto)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                int? loginResult = await _context.AppUsers
                    .Select(u => _context.Login(dto.Email, dto.Password))
                    .FirstOrDefaultAsync();
                if (loginResult == null)
                {
                    return Result.Fail(new Error("Email or password incorrect").WithMetadata("Code", "50030"));
                }

                return Result.Ok(loginResult.Value);
            });
        }

        public async Task<Result<AppUserDto>> GetAppUserAsyncAsync(int userId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var dto = await _context
                    .GetAppUser(userId)
                    .FirstOrDefaultAsync();

                if (dto == null)
                {
                    return Result.Fail(new Error("User not found").WithMetadata("Code", 50004));
                }

                return Result.Ok(dto);
            });
        }
    }
}
