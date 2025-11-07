using System.Security.Claims;
using AutoMapper;
using BookNest.Application.Dtos.Rooms;
using BookNest.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomsController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRoom(CreateRoomRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _roomService.CreateRoomAsync(_mapper.Map<CreateRoomDto>(request), GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
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
        public async Task<IActionResult> UpdateRoom(int id, RoomRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _roomService.UpdateRoomAsync(id, _mapper.Map<RoomDto>(request), GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _roomService.DeleteRoomAsync(id, GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return NoContent();
        }

        [HttpGet("by-hotel")]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!, int? guestsNumber = null!)
        {
            var result = await _roomService.GetRoomsByHotelAsync(hotelId, startDateTime, endDateTime, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var result = await _roomService.GetRoomAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }
    }
}
