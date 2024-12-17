using System.Security.Claims;
using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
using GusPizza.Infrastructure.Persistence.Repositories;
using GusPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GusPizza.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    [Authorize]
    public class TransactionController(ITransactionService service) : ControllerBase
    {
        private readonly ITransactionService transactionService = service;

        /// <summary>
        /// Create new transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TransactionDtoRequest request)
        {
            var response = CommonResponse<TransactionDtoResponse?>.commonResponse(
                StatusCodes.Status200OK,
                "Transaction created successfully",
                null
            );

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid generated token";
                return BadRequest(response);
            }

            response.Data = await transactionService.AddAsync(Guid.Parse(userId), request);
            return Ok(response);
        }

        /// <summary>
        /// Get all transaction
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var transactions = await transactionService.GetAllAsync();
            var response = CommonResponse<List<TransactionDtoResponse>>.commonResponse(
                StatusCodes.Status200OK,
                "List of transaction retrieved successfully",
                transactions
            );
            return Ok(response);
        }

        /// <summary>
        /// Get transaction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var transaction = await transactionService.GetByIdAsync(id);
            var response = CommonResponse<TransactionDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "Transaction retrieved successfully",
                transaction
            );
            return Ok(response);
        }

        /// <summary>
        /// Get list of transaction by logged user
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        public async Task<IActionResult> GetByUserIdAsync()
        {
            var response = CommonResponse<List<TransactionDtoResponse>?>.commonResponse(
                StatusCodes.Status200OK,
                "List of transaction retrieved successfully",
                null
            );

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid generated token";
                return BadRequest(response);
            }

            response.Data = await transactionService.GetByUserIdAsync(Guid.Parse(userId));
            return Ok(response);
        }
    }
}
