using System.Security.Claims;
using BookNest.Application.Dtos;
using BookNest.Application.Services;
using BookNest.Server.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public UsersController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _userService.Register(dto);

            if (!result.IsSuccess)
            {
                return Conflict(result.Errors);
            }

            var appUserDto = result.Value;

            return CreatedAtAction(
                nameof(GetAppUser),
                new { userId = appUserDto.AppUserId },
                new
                {
                    appUser = appUserDto,
                    jwtToken = _jwtTokenService.GenerateToken(appUserDto.AppUserId)
                }
            );
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAppUser(int userId)
        {
            var result = await _userService.GetAppUserAsync(userId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            var appUserDto = result.Value;
            return Ok(appUserDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var loginResult = await _userService.Login(dto);

            if (!loginResult.IsSuccess)
            {
                return Conflict(loginResult.Errors);
            }

            return Ok(new AuthResultResponseModel
            {
                JwtToken = _jwtTokenService.GenerateToken(loginResult.Value)
            });
        }
    }
}
