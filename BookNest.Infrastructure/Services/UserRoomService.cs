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
        private readonly IExecuteSafe _executeSafe;

        public UserRoomService(ApplicationDbContext dbContext, IExecuteSafe executeSafe)
        {
            _context = dbContext;
            _executeSafe = executeSafe;
        }

        public async Task<Result<RoomBookingDto>> GetRoomBookingsAsync(int appUserId, int roomId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var roomBooking = await _context
                    .GetRoomBooking(appUserId, roomId)
                    .FirstOrDefaultAsync();
                if (roomBooking == null)
                {
                    return Result.Fail(new Error("Room booking not found").WithMetadata("Code", 50017));
                }
                return Result.Ok(roomBooking);
            });
        }

        public async Task<Result<IEnumerable<RoomBookingByHotelDto>>> GetRoomBookingsByHotelAsync(int appUserId, int hotelId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                IEnumerable<RoomBookingByHotelDto> roomBookings = await _context
                    .GetRoomBookingsByHotel(appUserId, hotelId)
                    .ToListAsync();
                if (roomBookings == null)
                {
                    return Result.Fail(new Error("Room booking not found").WithMetadata("Code", 50017));
                }
                return Result.Ok(roomBookings);
            });
        }

        public async Task<Result<IEnumerable<AuditRoomBookingsByHotelDto>>> GetAuditRoomBookingsByHotelAsync(int appUserId, int hotelId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                IEnumerable<AuditRoomBookingsByHotelDto> roomBookings = await _context
                    .GetAuditRoomBookingsByHotel(appUserId, hotelId)
                    .ToListAsync();
                if (roomBookings == null)
                {
                    return Result.Fail(new Error("Room booking not found").WithMetadata("Code", 50017));
                }
                return Result.Ok(roomBookings);
            });
        }

        public async Task<Result<IEnumerable<RoomBookingByUserDto>>> GetRoomBookingsByUserAsync(int appUserId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                IEnumerable<RoomBookingByUserDto> hotelName = await _context
                    .GetRoomBookingsByUser(appUserId)
                    .ToListAsync();
                if (hotelName == null)
                {
                    return Result.Fail(new Error("Hotel not found").WithMetadata("Code", 50017));
                }
                return Result.Ok(hotelName);
            });
        }

        public async Task<Result<CreateAppUserRoomResultDto>> BookRoomAsync(int appUserId, BookingDto dto)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var result = await _context.CreateAppUserRooms
                    .FromSqlRaw("EXEC dbo.BookRoom @AppUserId, @RoomId, @StartDateTime, @EndDateTime",
                    new SqlParameter("@AppUserId", appUserId),
                    new SqlParameter("@RoomId", dto.RoomId),
                    new SqlParameter("@StartDateTime", dto.StartDate),
                    new SqlParameter("@EndDateTime", dto.EndDate))
                    .ToListAsync();

                return Result.Ok(result.First());
            });
        }

        public async Task<Result> UpdateRoomBookingAsync(int appUserId, BookingDto dto)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.UpdateRoomBooking @AppUserId, @RoomId, @StartDateTime, @EndDateTime",
                    new SqlParameter("@AppUserId", appUserId),
                    new SqlParameter("@RoomId", dto.RoomId),
                    new SqlParameter("@StartDateTime", dto.StartDate),
                    new SqlParameter("@EndDateTime", dto.EndDate));

                return Result.Ok();
            });
        }

        public async Task<Result> DeleteRoomBookingAsync(int appUserId, int roomId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.DeleteRoomBooking @AppUserId, @RoomId",
                new SqlParameter("@AppUserId", appUserId),
                new SqlParameter("@RoomId", roomId));

                return Result.Ok();
            });
        }
    }
}
