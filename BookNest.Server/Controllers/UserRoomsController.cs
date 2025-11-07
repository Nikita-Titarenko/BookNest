using System.Security.Claims;
using AutoMapper;
using BookNest.Application.Dtos.AppUserRooms;
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
    public class UserRoomsController : BaseController
    {
        private readonly IUserRoomService _userRoomService;
        private readonly IMapper _mapper;

        public UserRoomsController(IUserRoomService userRoomService, IMapper mapper)
        {
            _userRoomService = userRoomService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRoomBooking(int id)
        {
            var result = await _userRoomService.GetRoomBookingAsync(GetUserId(), id);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetRoomBookingsByUser()
        {
            var result = await _userRoomService.GetRoomBookingsByUserAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("by-hotel")]
        [Authorize]
        public async Task<IActionResult> GetRoomBookingsByHotel(int hotelId)
        {
            var result = await _userRoomService.GetRoomBookingsByHotelAsync(GetUserId(), hotelId);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("audit-by-hotel")]
        [Authorize]
        public async Task<IActionResult> GetAuditRoomBookingsByHotel(int hotelId)
        {
            var result = await _userRoomService.GetAuditRoomBookingsByHotelAsync(GetUserId(), hotelId);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BookRoom(BookingRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userRoomService.BookRoomAsync(GetUserId(), _mapper.Map<BookingDto>(request));
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            var appUserRoom = result.Value;
            return CreatedAtAction(
                nameof(GetRoomBooking),
                new { id = appUserRoom.RoomId },
                appUserRoom
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoomBooking(int id, BookingRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != request.RoomId)
            {
                return BadRequest(Result.Fail(new Error("Room id do not coincide").WithMetadata("Code", 50019)));
            }

            var result = await _userRoomService.UpdateRoomBookingAsync(GetUserId(), _mapper.Map<BookingDto>(request));
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRoomBooking(int id)
        {
            var result = await _userRoomService.DeleteRoomBookingAsync(GetUserId(), id);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return NoContent();
        }
    }
}
