using System.Security.Claims;
using AutoMapper;
using BookNest.Application.Dtos.Hotels;
using BookNest.Application.Services;
using BookNest.Server.Requests.Hotels;
using BookNest.Server.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : BaseController
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelsController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet("by-user")]
        [Authorize]
        public async Task<IActionResult> GetHotelsByUser()
        {
            var result = await _hotelService.GetHotelsByUserAsync(GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}/name")]
        public async Task<IActionResult> GetHotelName(int id)
        {
            var result = await _hotelService.GetHotelNameAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(new GetHotelNameResponseModel
            {
                HotelName = result.Value
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var result = await _hotelService.GetHotelAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("with-cheapest-rooms")]
        public async Task<IActionResult> GetHotelsWithCheapestRoom(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            var result = await _hotelService.GetHotelsWithCheapestRoomsAsync(startDate, endDate, pageNumber, pageSize, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("with-most-expensive-rooms")]
        public async Task<IActionResult> GetHotelsWithMostExpensiveRoom(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        {
            var result = await _hotelService.GetHotelsWithMostExpensiveRoomsAsync(startDate, endDate, pageNumber, pageSize, guestsNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHotel(HotelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _hotelService.CreateHotelAsync(GetUserId(), _mapper.Map<HotelDto>(request));
            if (!result.IsSuccess)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsValue(50004)))
                {
                    return NotFound(result.Errors);
                }

                return BadRequest(MapErrors(result.Errors));
            }

            var hotelDto = result.Value;
            return CreatedAtAction(
                nameof(GetHotel),
                new { id = result.Value.HotelId },
                hotelDto
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditHotel(int id, HotelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _hotelService.UpdateHotelAsync(GetUserId(), id, _mapper.Map<HotelDto>(request));
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
            var result = await _hotelService.DeleteHotelAsync(id, GetUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(MapErrors(result.Errors));
            }

            return NoContent();
        }
    }
}
