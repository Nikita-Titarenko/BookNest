using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IUserRoomService
    {
        Task<Result> BookRoomAsync(int appUserId, int roomId, BookingDto dto);
        Task<Result> UpdateRoomBookingAsync(int appUserId, int roomId, BookingDto dto);
    }
}