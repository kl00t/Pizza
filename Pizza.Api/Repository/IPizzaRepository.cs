﻿namespace Pizza.Api.Repository;

public interface IPizzaRepository
{
    IEnumerable<Models.Pizza> GetAll();
    Models.Pizza GetById(Guid id);
    void Create(Models.Pizza pizza);
    void Update(Models.Pizza pizza);
    void Delete(Guid id);
}

public class PizzaRepository : IPizzaRepository
{
    private readonly List<Models.Pizza> _pizzas = new();

    public IEnumerable<Models.Pizza> GetAll()
    {
        return _pizzas;
    }

    public Models.Pizza GetById(Guid id)
    {
        return _pizzas.FirstOrDefault(p => p.Id == id);
    }

    public void Create(Models.Pizza pizza)
    {
        _pizzas.Add(pizza);
    }

    public void Update(Models.Pizza pizza)
    {
        var existingPizza = GetById(pizza.Id);
        if (existingPizza != null)
        {
            existingPizza.Name = pizza.Name;
            existingPizza.Size = pizza.Size;
        }
    }

    public void Delete(Guid id)
    {
        var pizza = GetById(id);
        if (pizza != null)
        {
            _pizzas.Remove(pizza);
        }
    }
}
