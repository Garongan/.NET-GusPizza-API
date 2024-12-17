using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
using GusPizza.Infrastructure.Security;
using GusPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GusPizza.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IUserService service, IJwtTokenGenerator jwtToken) : ControllerBase
    {
        private readonly IUserService userService = service;
        private readonly IJwtTokenGenerator jwtTokenGenerator = jwtToken;

        /// <summary>
        /// Login account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDtoRequest request)
        {
            var user = await userService.AuthenticateAsync(request.Username, request.Password);
            var response = CommonResponse<LoginDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "Login successfully",
                new LoginDtoResponse("", "")
            );

            if (user == null)
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Invalid username or password";
                return Unauthorized(response);
            }

            var token = jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Role);
            response.Data = new LoginDtoResponse(token, user.Role.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDtoRequest request)
        {
            var user = await userService.AddAsync(request.Username, request.Password);
            var response = CommonResponse<UserDtoResponse>.commonResponse(
                StatusCodes.Status201Created,
                "User created successfully",
                user
            );
            return Created($"api/auth/{user.Id}", response);
        }
    }
}
