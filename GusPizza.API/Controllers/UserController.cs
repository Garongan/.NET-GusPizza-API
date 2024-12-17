using System.Security.Claims;
using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid generated token";
                return BadRequest(response);
            }

            response.Data = await userService.GetByIdAsync(Guid.Parse(userId));
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid generated token";
                return BadRequest(response);
            }

            response.Data = await userService.UpdateAsync(Guid.Parse(userId), request.Username);
            return Ok(response);
        }

        /// <summary>
        /// Get all user by admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await userService.GetAllAsync();
            var response = CommonResponse<List<UserDtoResponse>>.commonResponse(
                StatusCodes.Status200OK,
                "List of user retrieved successfully",
                users
            );
            return Ok(response);
        }
    }
}
