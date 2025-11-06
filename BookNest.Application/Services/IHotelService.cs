using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IHotelService
    {
        Task<Result<CreateHotelResultDto>> CreateHotelAsync(int appUserId, HotelDto hotelDto);
        Task<Result> DeleteHotelAsync(int hotelId, int appUserId);
        Task<Result<HotelDto>> GetHotelAsync(int hotelId);
        Task<Result<string>> GetHotelNameAsync(int hotelId);
        Task<Result<IEnumerable<HotelListItemDto>>> GetHotelsByUserAsync(int userId);
        Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithCheapestRoomsAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null);
        Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithMostExpensiveRoomsAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null);
        Task<Result> UpdateHotelAsync(int appUserId, int hotelId, HotelDto hotelDto);
    }
}