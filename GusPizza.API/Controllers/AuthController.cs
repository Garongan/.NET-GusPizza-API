using GusPizza.Application.Dto;
using GusPizza.Application.Services.Interfaces;
using GusPizza.Domain;
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDtoRequest request)
        {
            var user = await userService.AuthenticateAsync(request.Username, request.Password);
            var response = CommonResponse<LoginDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "",
                new LoginDtoResponse("", "")
            );

            if (user == null)
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Invalid username or password";
                return Unauthorized(response);
            }

            var token = jwtTokenGenerator.GenerateToken(user.Username, user.Role);
            response.Data = new LoginDtoResponse(token, user.Role.ToString());

            return Ok(response);
        }
    }
}
