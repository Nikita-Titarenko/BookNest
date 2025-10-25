using System.Data;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Result<int>> CreateRoom(CreateRoomDto dto, int appUserId)
        {
            var roomIdParameter = new SqlParameter("@RoomId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };

            await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.CreateRoom @RoomName, @RoomDescription, @RoomPrice, @RoomQuantity, @GuestsNumber, @RoomSize, @HotelId, @AppUserId, @RoomId OUTPUT",
                    new SqlParameter("@RoomName", dto.RoomName),
                    new SqlParameter("@RoomDescription", dto.RoomDescription),
                    new SqlParameter("@RoomPrice", dto.RoomPrice),
                    new SqlParameter("@RoomQuantity", dto.RoomQuantity),
                    new SqlParameter("@GuestsNumber", dto.GuestsNumber),
                    new SqlParameter("@RoomSize", dto.RoomSize),
                    new SqlParameter("@HotelId", dto.HotelId),
                    new SqlParameter("@AppUserId", appUserId),
                    roomIdParameter);

            return Result.Ok((int)roomIdParameter.Value);
        }

        public async Task<Result> UpdateRoom(int roomId, RoomDto dto, int appUserId)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.UpdateRoom @RoomId, @RoomName, @RoomDescription, @RoomPrice, @RoomQuantity, @GuestsNumber, @RoomSize, @AppUserId",
                    new SqlParameter("@RoomId", roomId),
                    new SqlParameter("@RoomName", dto.RoomName),
                    new SqlParameter("@RoomDescription", dto.RoomDescription),
                    new SqlParameter("@RoomPrice", dto.RoomPrice),
                    new SqlParameter("@RoomQuantity", dto.RoomQuantity),
                    new SqlParameter("@GuestsNumber", dto.GuestsNumber),
                    new SqlParameter("@RoomSize", dto.RoomSize),
                    new SqlParameter("@AppUserId", appUserId)
                );

            return Result.Ok();
        }

        public async Task<Result<int>> DeleteRoom(int roomId, int appUserId)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.DeleteRoom @RoomId, @AppUserId",
                new SqlParameter("@RoomId", roomId),
                new SqlParameter("@AppUserId", appUserId));

            return Result.Ok();
        }

        public async Task<Result<IEnumerable<RoomListItemDto>>> GetRoomsByHotel(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!)
        {
            IEnumerable<RoomListItemDto> dto = await _context
                .GetRoomsByHotel(hotelId, startDateTime, endDateTime)
                .ToListAsync();

            return Result.Ok(dto);
        }
    }
}
