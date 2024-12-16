using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GusPizza.Application.Dto;
using GusPizza.Application.Services;
using GusPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GusPizza.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService service) : ControllerBase
    {
        private readonly IUserService userService = service;

        /// <summary>
        /// Get detail of logged account
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        public async Task<IActionResult> GetLoggedUser()
        {
            var response = CommonResponse<UserDtoResponse?>.commonResponse(
                StatusCodes.Status200OK,
                "User retrieved successfully",
                null
            );

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid generated token";
                return BadRequest(response);
            }

            var user = await userService.GetByIdAsync(Guid.Parse(userId));
            response.Data = new UserDtoResponse(user.Id, user.Username, user.Role, user.CreatedAt, user.UpdatedAt);
            return Ok(response);
        }

        /// <summary>
        /// Update detail of logged account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDtoRequest request)
        {
            var response = CommonResponse<UserDtoResponse?>.commonResponse(
                StatusCodes.Status200OK,
                "User updated successfully",
                null
            );

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid generated token";
                return BadRequest(response);
            }

            var user = await userService.UpdateAsync(Guid.Parse(userId), request.Username);
            response.Data = new UserDtoResponse(user.Id, user.Username, user.Role, user.CreatedAt, user.UpdatedAt);
            return Ok(response);
        }
    }
}
