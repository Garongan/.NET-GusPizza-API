using GusPizza.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GusPizza.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController(PizzaService pizzaService) : ControllerBase
    {
        private readonly PizzaService _pizzaService = pizzaService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzas = await _pizzaService.GetAll();
            return Ok(pizzas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PizzaDto pizzaDto)
        {
            await _pizzaService.Create(pizzaDto.Name, pizzaDto.Price);
            return CreatedAtAction(nameof(GetAll), null);
        }
    }
}
