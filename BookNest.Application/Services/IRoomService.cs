using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IRoomService
    {
        Task<Result<int>> CreateRoomAsync(CreateRoomDto dto, int appUserId);
        Task<Result<int>> DeleteRoomAsync(int roomId, int appUserId);
        Task<Result<RoomListItemDto>> GetRoomAsync(int roomId);
        Task<Result<IEnumerable<RoomListItemDto>>> GetRoomsByHotelAsync(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!, int? guestsNumber = null!);
        Task<Result> UpdateRoomAsync(int roomId, RoomDto dto, int appUserId);
    }
}