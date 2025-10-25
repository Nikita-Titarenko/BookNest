using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IRoomService
    {
        Task<Result<int>> CreateRoom(CreateRoomDto dto, int appUserId);
        Task<Result<int>> DeleteRoom(int roomId, int appUserId);
        Task<Result<IEnumerable<RoomListItemDto>>> GetRoomsByHotel(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!);
        Task<Result> UpdateRoom(int roomId, RoomDto dto, int appUserId);
    }
}