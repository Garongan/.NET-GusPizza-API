using GusPizza.Application;
using GusPizza.Application.Services;
using GusPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GusPizza.API.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController(IPizzaService service) : ControllerBase
    {
        private readonly IPizzaService pizzaService = service;

        /// <summary>
        /// Get all pizza
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllAsync(bool isDeleted)
        {
            var pizzas = await pizzaService.GetAllAsync(isDeleted);
            var responses = pizzas.Select(p => new PizzaDtoResponse(p.Id, p.Name, p.Price, p.IsAvailable, p.CreatedAt, p.UpdatedAt, p.DeletedAt)).ToList();
            var response = CommonResponse<List<PizzaDtoResponse>>.commonResponse(
                StatusCodes.Status200OK,
                "List of pizzas retrieved successfully",
                responses
            );
            return Ok(response);
        }

        /// <summary>
        /// Create new pizza
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] PizzaDtoRequest request)
        {
            var pizza = await pizzaService.CreateAsync(request.Name, request.Price);
            var response = CommonResponse<PizzaDtoResponse>.commonResponse(
                StatusCodes.Status201Created,
                "Pizza created successfully",
                new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable, pizza.CreatedAt, pizza.UpdatedAt, pizza.DeletedAt)
            );
            return Created($"api/pizzas/{pizza.Id}", response);
        }

        /// <summary>
        /// Get pizza by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var pizza = await pizzaService.GetByIdAsync(id);
            var response = CommonResponse<PizzaDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "Pizzas retrieved successfully",
                new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable, pizza.CreatedAt, pizza.UpdatedAt, pizza.DeletedAt)
            );
            return Ok(response);
        }

        /// <summary>
        /// Update pizza
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PizzaDtoRequest request)
        {
            var pizza = await pizzaService.UpdateAsync(id, request.Name, request.Price);
            var response = CommonResponse<PizzaDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "Pizza updated successfully",
                new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable, pizza.CreatedAt, pizza.UpdatedAt, pizza.DeletedAt)
            );
            return Ok(response);
        }

        /// <summary>
        /// Delete pizza
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await pizzaService.DeleteAsync(id);
            var response = CommonResponse<string>.commonResponse(
                StatusCodes.Status200OK,
                "Pizza deleted successfully",
                ""
            );
            return Ok(response);
        }
    }
}
