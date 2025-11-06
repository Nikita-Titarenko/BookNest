using System.Data;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;
        private readonly IExecuteSafe _executeSafe;

        public HotelService(ApplicationDbContext context, IExecuteSafe executeSafe)
        {
            _context = context;
            _executeSafe = executeSafe;
        }

        public async Task<Result<string>> GetHotelNameAsync(int hotelId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var hotelName = await _context.AppUsers
                    .Select(h => _context.GetHotelName(hotelId))
                    .FirstOrDefaultAsync();
                if (hotelName == null)
                {
                    return Result.Fail(new Error("Hotel not found").WithMetadata("Code", 50017));
                }
                return Result.Ok(hotelName);
            });
        }

        public async Task<Result<HotelDto>> GetHotelAsync(int hotelId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var dto = await _context
                    .GetHotel(hotelId)
                    .FirstOrDefaultAsync();

                if (dto == null)
                {
                    return Result.Fail(new Error("Hotel not found").WithMetadata("Code", 50017));
                }

                return Result.Ok(dto);
            });
        }

        public async Task<Result<IEnumerable<HotelListItemDto>>> GetHotelsByUserAsync(int userId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                IEnumerable<HotelListItemDto> hotels = await _context
                    .GetHotelsByUser(userId)
                    .ToListAsync();

                if (hotels == null)
                {
                    return Result.Fail(new Error(""));
                }

                return Result.Ok(hotels);
            });
        }

        public async Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithCheapestRoomsAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                IEnumerable<HotelWithRoomListItemDto> hotels = await _context
                    .GetHotelsWithCheapestRoom(startDate, endDate, pageNumber, pageSize, guestsNumber)
                    .ToListAsync();

                if (hotels == null)
                {
                    return Result.Fail(new Error(""));
                }

                return Result.Ok(hotels);
            });
        }

        public async Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithMostExpensiveRoomsAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            IEnumerable<HotelWithRoomListItemDto> hotels = await _context
                .GetHotelsWithMostExpensiveRoom(startDate, endDate, pageNumber, pageSize, guestsNumber)
                .ToListAsync();

            if (hotels == null)
            {
                return Result.Fail(new Error(""));
            }

            return Result.Ok(hotels);
        }

        public async Task<Result<CreateHotelResultDto>> CreateHotelAsync(int appUserId, HotelDto hotelDto)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var result = await _context.CreateHotelResults
                    .FromSqlRaw("EXEC dbo.CreateHotel @AppUserId, @HotelName, @HotelDescription, @HotelCity",
                        new SqlParameter("@AppUserId", appUserId),
                        new SqlParameter("@HotelName", hotelDto.HotelName),
                        new SqlParameter("@HotelDescription", hotelDto.HotelDescription),
                        new SqlParameter("@HotelCity", hotelDto.HotelCity)
                    )
                    .ToListAsync();

                return Result.Ok(result.First());
            });
        }

        public async Task<Result> UpdateHotelAsync(int appUserId, int hotelId, HotelDto hotelDto)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.UpdateHotel @HotelId, @AppUserId, @HotelName, @HotelDescription, @HotelCity",
                        new SqlParameter("@HotelId", hotelId),
                        new SqlParameter("@AppUserId", appUserId),
                        new SqlParameter("@HotelName", hotelDto.HotelName),
                        new SqlParameter("@HotelDescription", hotelDto.HotelDescription),
                        new SqlParameter("@HotelCity", hotelDto.HotelCity));

                return Result.Ok();
            });
        }

        public async Task<Result> DeleteHotelAsync(int hotelId, int appUserId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.DeleteHotel @HotelId, @AppUserId",
                        new SqlParameter("@AppUserId", appUserId),
                        new SqlParameter("@HotelId", hotelId));

                return Result.Ok();
            });
        }
    }
}
