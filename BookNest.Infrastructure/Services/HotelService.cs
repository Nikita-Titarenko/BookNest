using System.Data;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
using FluentResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<HotelListItemDto>>> GetHotelsByUser(int userId)
        {
            try
            {
                IEnumerable<HotelListItemDto> hotels = await _context
                    .GetHotelsByUser(userId)
                    .ToListAsync();

                if (hotels == null)
                {
                    return Result.Fail(new Error(""));
                }

                return Result.Ok(hotels);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", 1));
            }
        }

        public async Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithCheapestRooms(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            try {
                IEnumerable<HotelWithRoomListItemDto> hotels = await _context
                    .GetHotelsWithCheapestRoom(startDate, endDate, pageNumber, pageSize, guestsNumber)
                    .ToListAsync();

                if (hotels == null)
                {
                    return Result.Fail(new Error(""));
                }

                return Result.Ok(hotels);
            }
            catch (SqlException ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
        }

        public async Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithMostExpensiveRooms(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
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

        public async Task<Result<int>> CreateHotel(int appUserId, HotelDto hotelDto)
        {
            try
            {
                var hotelIdParameter = new SqlParameter("@HotelId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output,
                };

                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.CreateHotel @AppUserId, @HotelName, @HotelDescription, @HotelCity, @HotelId OUTPUT",
                        new SqlParameter("@AppUserId", appUserId),
                        new SqlParameter("@HotelName", hotelDto.HotelName),
                        new SqlParameter("@HotelDescription", hotelDto.HotelDescription),
                        new SqlParameter("@HotelCity", hotelDto.HotelCity),
                        hotelIdParameter);

                return Result.Ok((int)hotelIdParameter.Value);
            }
            catch (SqlException ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
        }

        public async Task<Result> UpdateHotel(int appUserId, int hotelId, HotelDto hotelDto)
        {
            try
            {
                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.UpdateHotel @HotelId, @AppUserId, @HotelName, @HotelDescription, @HotelCity",
                        new SqlParameter("@HotelId", hotelId),
                        new SqlParameter("@AppUserId", appUserId),
                        new SqlParameter("@HotelName", hotelDto.HotelName),
                        new SqlParameter("@HotelDescription", hotelDto.HotelDescription),
                        new SqlParameter("@HotelCity", hotelDto.HotelCity));

                return Result.Ok();
            }
            catch (SqlException ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
        }

        public async Task<Result> DeleteHotel(int hotelId, int appUserId)
        {
            try
            {
                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.DeleteHotel @HotelId, @AppUserId",
                        new SqlParameter("@AppUserId", appUserId),
                        new SqlParameter("@HotelId", hotelId));

                return Result.Ok();
            }
            catch (SqlException ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("Code", ex.Number));
            }
        }
    }
}
