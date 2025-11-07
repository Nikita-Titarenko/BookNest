using System.Security.Claims;
using BookNest.Application.Dtos.Rooms;
using BookNest.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRoom(CreateRoomDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _roomService.CreateRoomAsync(dto, Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            var room = result.Value;

            return CreatedAtAction(
                nameof(GetRoom),
                new { id = room.RoomId },
                room
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoom(int id, RoomDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _roomService.UpdateRoomAsync(id, dto, Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _roomService.DeleteRoomAsync(id, Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet("by-hotel")]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!, int? guestsNumber = null!)
        {
            var result = await _roomService.GetRoomsByHotelAsync(hotelId, startDateTime, endDateTime, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var result = await _roomService.GetRoomAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
