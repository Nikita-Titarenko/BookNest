using System;
using System.Data;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Register(RegisterDto dto)
        {
            try
            {
                var userIdParameter = new SqlParameter("@UserId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.Register @Fullname, @Email, @Phone, @Password, @UserId OUTPUT",
                    new SqlParameter("@Fullname", dto.Fullname),
                    new SqlParameter("@Email", dto.Email),
                    new SqlParameter("@Phone", dto.Phone),
                    new SqlParameter("@Password", dto.Password),
                    userIdParameter
                );

                return Result.Ok((int)userIdParameter.Value);
            }
            catch (SqlException ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
        }

        public async Task<Result<int>> Login(LoginDto dto)
        {
            int? loginResult = await _context.AppUsers
                .Select(u => _context.Login(dto.Email, dto.Password))
                .FirstOrDefaultAsync();
            if (loginResult == null)
            {
                return Result.Fail(new Error("Email or password incorrect").WithMetadata("Code", "50030"));
            }

            return Result.Ok(loginResult.Value);
        }
    }
}
