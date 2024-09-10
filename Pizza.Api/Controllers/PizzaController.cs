
using Microsoft.AspNetCore.Mvc;
using Pizza.Api.Repository;


namespace Pizza.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IRepository<Models.Pizza> _pizzaRepository;
    private readonly IRepository<Models.Burger> _burgerRepository;

    public PizzaController(IRepository<Models.Pizza> pizzaRepository, IRepository<Models.Burger> burgerRepository)
    {
        _pizzaRepository = pizzaRepository;
        _burgerRepository = burgerRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var pizzas = _pizzaRepository.GetAll();
        return Ok(pizzas);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var pizza = _pizzaRepository.GetById(id);
        if (pizza == null)
        {
            return NotFound();
        }
        return Ok(pizza);
    }

    [HttpPost]
    public IActionResult Create(Models.Pizza pizza)
    {
        _pizzaRepository.Create(pizza);
        return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Models.Pizza pizza)
    {
        if (id != pizza.Id)
        {
            return BadRequest();
        }

        var existingPizza = _pizzaRepository.GetById(id);
        if (existingPizza == null)
        {
            return NotFound();
        }

        _pizzaRepository.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var pizza = _pizzaRepository.GetById(id);
        if (pizza == null)
        {
            return NotFound();
        }

        _pizzaRepository.Delete(id);
        return NoContent();
    }
}
