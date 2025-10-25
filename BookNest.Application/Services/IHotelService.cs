using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IHotelService
    {
        Task<Result<int>> CreateHotel(int appUserId, HotelDto hotelDto);
        Task<Result> DeleteHotel(int hotelId, int appUserId);
        Task<Result<IEnumerable<HotelListItemDto>>> GetHotelsByUser(int userId);
        Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithCheapestRooms(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null);
        Task<Result<IEnumerable<HotelWithRoomListItemDto>>> GetHotelsWithMostExpensiveRooms(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null);
        Task<Result> UpdateHotel(int appUserId, int hotelId, HotelDto hotelDto);
    }
}