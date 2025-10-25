using System.Security.Claims;
using BookNest.Application.Dtos;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetHotelsByUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.GetHotelsByUser(Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("with-cheapest-rooms")]
        public async Task<IActionResult> GetHotelsWithCheapestRoom(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            var result = await _hotelService.GetHotelsWithCheapestRooms(startDate, endDate, pageNumber, pageSize, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("with-most-expensive-rooms")]
        public async Task<IActionResult> GetHotelsWithMostExpensiveRoom(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            var result = await _hotelService.GetHotelsWithMostExpensiveRooms(startDate, endDate, pageNumber, pageSize, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateHotel(HotelDto hotelDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.CreateHotel(Convert.ToInt32(userId), hotelDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new CreateHotelResponseModel
            {
                HotelId = result.Value
            });
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EditHotel(int id, HotelDto hotelDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.UpdateHotel(Convert.ToInt32(userId), id, hotelDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _hotelService.DeleteHotel(id, Convert.ToInt32(userId));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
