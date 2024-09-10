using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pizza.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IRepository<Pizza.Api.Models.Pizza>, PizzaRepository>();
// OR
builder.Services.AddSingleton<IRepository<Pizza.Api.Models.Burger>, Repository<Pizza.Api.Models.Burger>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();