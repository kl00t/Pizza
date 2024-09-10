namespace Pizza.Api.Models;

public class Burger
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public DateTime DateCreated { get; set; }
}