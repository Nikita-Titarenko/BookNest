using System.Security.Claims;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
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

        [HttpPost("{room_id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BookRoom(int roomId, BookingDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _userRoomService.BookRoomAsync(Convert.ToInt32(userId), roomId, dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
