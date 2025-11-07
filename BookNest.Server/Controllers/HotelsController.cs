using System.Security.Claims;
using BookNest.Application.Dtos.Hotels;
using BookNest.Application.Services;
using BookNest.Server.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetHotelsByUserAsync()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.GetHotelsByUserAsync(Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}/name")]
        public async Task<IActionResult> GetHotelNameAsync(int id)
        {
            var result = await _hotelService.GetHotelNameAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new GetHotelNameResponseModel
            {
                HotelName = result.Value
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelAsync(int id)
        {
            var result = await _hotelService.GetHotelAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("with-cheapest-rooms")]
        public async Task<IActionResult> GetHotelsWithCheapestRoomAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            var result = await _hotelService.GetHotelsWithCheapestRoomsAsync(startDate, endDate, pageNumber, pageSize, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("with-most-expensive-rooms")]
        public async Task<IActionResult> GetHotelsWithMostExpensiveRoomAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            var result = await _hotelService.GetHotelsWithMostExpensiveRoomsAsync(startDate, endDate, pageNumber, pageSize, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHotelAsync(HotelDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.CreateHotelAsync(Convert.ToInt32(userId), dto);
            if (!result.IsSuccess)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsValue("50004")))
                {
                    return NotFound(result.Errors);
                }

                return BadRequest(result.Errors);
            }

            var hotelDto = result.Value;
            return CreatedAtAction(
                nameof(GetHotelAsync), 
                new { id = result.Value.HotelId },
                hotelDto
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditHotelAsync(int id, HotelDto hotelDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.UpdateHotelAsync(Convert.ToInt32(userId), id, hotelDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotelAsync(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.DeleteHotelAsync(id, Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
