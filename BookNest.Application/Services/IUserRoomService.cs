using BookNest.Application.Dtos;
using FluentResults;

namespace BookNest.Application.Services
{
    public interface IUserRoomService
    {
        Task<Result<CreateAppUserRoomResultDto>> BookRoomAsync(int appUserId, BookingDto dto);
        Task<Result> DeleteRoomBookingAsync(int appUserId, int roomId);
        Task<Result<IEnumerable<AuditRoomBookingsByHotelDto>>> GetAuditRoomBookingsByHotelAsync(int appUserId, int hotelId);
        Task<Result<RoomBookingDto>> GetRoomBookingsAsync(int appUserId, int roomId);
        Task<Result<IEnumerable<RoomBookingByHotelDto>>> GetRoomBookingsByHotelAsync(int appUserId, int hotelId);
        Task<Result<IEnumerable<RoomBookingByUserDto>>> GetRoomBookingsByUserAsync(int appUserId);
        Task<Result> UpdateRoomBookingAsync(int appUserId, BookingDto dto);
    }
}