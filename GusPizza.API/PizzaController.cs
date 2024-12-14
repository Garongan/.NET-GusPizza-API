using GusPizza.Application;
using GusPizza.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GusPizza.API
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController(PizzaService service) : ControllerBase
    {
        private readonly PizzaService pizzaService = service;

        /// <summary>
        /// Get all pizza
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzas = await pizzaService.GetAll();
            var responses = pizzas.Select(p => new PizzaDtoResponse(p.Id, p.Name, p.Price, p.IsAvailable)).ToList();
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
        public async Task<IActionResult> Create([FromBody] PizzaDtoRequest request)
        {
            var pizza = await pizzaService.Create(request.Name, request.Price);
            var response = CommonResponse<PizzaDtoResponse>.commonResponse(
                StatusCodes.Status201Created,
                "Pizza created successfully",
                new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable)
            );
            return Created($"api/pizzas/{pizza.Id}", response);
        }

        /// <summary>
        /// Get pizza by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pizza = await pizzaService.GetById(id);
            var response = CommonResponse<PizzaDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "Pizzas retrieved successfully",
                new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable)
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
        public async Task<IActionResult> Update(Guid id, [FromBody] PizzaDtoRequest request)
        {
            var pizza = await pizzaService.Update(id, request.Name, request.Price);
            var response = CommonResponse<PizzaDtoResponse>.commonResponse(
                StatusCodes.Status200OK,
                "Pizza updated successfully",
                new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable)
            );
            return Ok(response);
        }

        /// <summary>
        /// Delete pizza
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await pizzaService.Delete(id);
            var response = CommonResponse<string>.commonResponse(
                StatusCodes.Status200OK,
                "Pizza deleted successfully",
                ""
            );
            return Ok(response);
        }
    }
}
