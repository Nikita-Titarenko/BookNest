using BookNest.Application.Dtos;
using BookNest.Application.Services;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Services
{
    public class UserRoomService : IUserRoomService
    {
        private readonly ApplicationDbContext _context;

        public UserRoomService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result> BookRoomAsync(int appUserId, int roomId, BookingDto dto)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.BookRoom @AppUserId, @RoomId, @StartDateTime, @EndDateTime",
                new SqlParameter("@AppUserId", appUserId),
                new SqlParameter("@RoomId", roomId),
                new SqlParameter("@StartDateTime", roomId),
                new SqlParameter("@EndDateTime", dto.EndDateTime));

            return Result.Ok();
        }

        public async Task<Result> UpdateRoomBookingAsync(int appUserId, int roomId, BookingDto dto)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.UpdateRoomBooking @AppUserId, @RoomId, @StartDateTime, @EndDateTime",
                new SqlParameter("@AppUserId", appUserId),
                new SqlParameter("@RoomId", roomId),
                new SqlParameter("@StartDateTime", roomId),
                new SqlParameter("@EndDateTime", dto.EndDateTime));

            return Result.Ok();
        }

        public async Task<Result> DeleteRoomBookingAsync(int appUserId, int roomId, BookingDto dto)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.DeleteRoomBooking @AppUserId, @RoomId",
                new SqlParameter("@AppUserId", appUserId),
                new SqlParameter("@RoomId", roomId));

            return Result.Ok();
        }
    }
}
