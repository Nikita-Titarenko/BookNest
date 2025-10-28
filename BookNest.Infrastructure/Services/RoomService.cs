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
        private readonly IExecuteSafe _executeSafe;

        public RoomService(ApplicationDbContext context, IExecuteSafe executeSafe) {
            _context = context;
            _executeSafe = executeSafe;
        }

        public async Task<Result<int>> CreateRoomAsync(CreateRoomDto dto, int appUserId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var roomIdParameter = new SqlParameter("@RoomId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output,
                };

                await _context.Database
                    .ExecuteSqlRawAsync("EXEC dbo.CreateRoom @RoomName, @RoomPrice, @RoomQuantity, @GuestsNumber, @RoomSize, @HotelId, @AppUserId, @RoomId OUTPUT",
                        new SqlParameter("@RoomName", dto.RoomName),
                        new SqlParameter("@RoomPrice", dto.RoomPrice),
                        new SqlParameter("@RoomQuantity", dto.RoomQuantity),
                        new SqlParameter("@GuestsNumber", dto.GuestsNumber),
                        new SqlParameter("@RoomSize", dto.RoomSize),
                        new SqlParameter("@HotelId", dto.HotelId),
                        new SqlParameter("@AppUserId", appUserId),
                        roomIdParameter);

                return Result.Ok((int)roomIdParameter.Value);
            });
        }

        public async Task<Result> UpdateRoomAsync(int roomId, RoomDto dto, int appUserId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.UpdateRoom @RoomId, @RoomName, @RoomPrice, @RoomQuantity, @GuestsNumber, @RoomSize, @AppUserId",
                    new SqlParameter("@RoomId", roomId),
                    new SqlParameter("@RoomName", dto.RoomName),
                    new SqlParameter("@RoomPrice", dto.RoomPrice),
                    new SqlParameter("@RoomQuantity", dto.RoomQuantity),
                    new SqlParameter("@GuestsNumber", dto.GuestsNumber),
                    new SqlParameter("@RoomSize", dto.RoomSize),
                    new SqlParameter("@AppUserId", appUserId)
                );

                return Result.Ok();
            });
        }

        public async Task<Result<int>> DeleteRoomAsync(int roomId, int appUserId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                await _context.Database
                .ExecuteSqlRawAsync("EXEC dbo.DeleteRoom @RoomId, @AppUserId",
                new SqlParameter("@RoomId", roomId),
                new SqlParameter("@AppUserId", appUserId));

                return Result.Ok();
            });
        }

        public async Task<Result<IEnumerable<RoomListItemDto>>> GetRoomsByHotelAsync(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!, int? guestsNumber = null!)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                IEnumerable<RoomListItemDto> dto = await _context
                    .GetRoomsByHotel(hotelId, startDateTime, endDateTime, guestsNumber)
                    .ToListAsync();
                return Result.Ok(dto);
            });
        }

        public async Task<Result<RoomListItemDto>> GetRoomAsync(int roomId)
        {
            return await _executeSafe.ExecuteSafeAsync(async () =>
            {
                var dto = await _context
                    .GetRoom(roomId)
                    .FirstOrDefaultAsync();
                if (dto == null)
                {
                    return Result.Fail(new Error("Room not found").WithMetadata("Code", 50018));
                }
                return Result.Ok(dto);
            });
        }
    }
}
