using AutoMapper;
using BookNest.Application.Dtos.AppUsers;
using BookNest.Application.Services;
using BookNest.Server.Requests.AppUsers;
using BookNest.Server.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.RegisterAsync(_mapper.Map<RegisterDto>(request));

            if (!result.IsSuccess)
            {
                return Conflict(MapErrors(result.Errors));
            }

            CreateAppUserResultDto appUserDto = result.Value;

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
            var result = await _userService.GetAppUserAsyncAsync(userId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            var appUserDto = result.Value;
            return Ok(appUserDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.LoginAsync(_mapper.Map<LoginDto>(request));

            if (!result.IsSuccess)
            {
                return Conflict(MapErrors(result.Errors));
            }

            return Ok(new AuthResultResponseModel
            {
                JwtToken = _jwtTokenService.GenerateToken(result.Value)
            });
        }
    }
}
