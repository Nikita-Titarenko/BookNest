using System.Security.Claims;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
using BookNest.Infrastructure.Services;
using FluentResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoomsController : ControllerBase
    {
        private readonly IUserRoomService _userRoomService;

        public UserRoomsController(IUserRoomService userRoomService)
        {
            _userRoomService = userRoomService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRoomBookings(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userRoomService.GetRoomBookingsAsync(Convert.ToInt32(userId), id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetRoomBookingsByUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userRoomService.GetRoomBookingsByUserAsync(Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("by-hotel")]
        [Authorize]
        public async Task<IActionResult> GetRoomBookingsByHotel(int hotelId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userRoomService.GetRoomBookingsByHotelAsync(Convert.ToInt32(userId), hotelId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("audit-by-hotel")]
        [Authorize]
        public async Task<IActionResult> GetAuditRoomBookingsByHotel(int hotelId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _userRoomService.GetAuditRoomBookingsByHotelAsync(Convert.ToInt32(userId), hotelId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BookRoom(BookingDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _userRoomService.BookRoomAsync(Convert.ToInt32(userId), dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var appUserRoom = result.Value;
            return CreatedAtAction(
                nameof(GetRoomBookings),
                new { id = appUserRoom.RoomId },
                appUserRoom
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoomBooking(int id, BookingDto dto)
        {
            if (id != dto.RoomId)
            {
                return BadRequest(Result.Fail(new Error("Room id do not coincide").WithMetadata("Code", 50019)));
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _userRoomService.UpdateRoomBookingAsync(Convert.ToInt32(userId), dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRoomBooking(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _userRoomService.DeleteRoomBookingAsync(Convert.ToInt32(userId), id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
