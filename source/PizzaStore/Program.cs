using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Context;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var connectionString = config.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

#region Services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PizzaStore API",
        Description = "Making the Pizzas you love",
        Version = "v1"
    });
});

builder.Services.AddDbContext<PizzaContext>(
    options =>
    {
        options.UseSqlite(connectionString);
    });
#endregion

var app = builder.Build();

#region Configurations for Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}
#endregion

#region Endpoints
app.MapGet("/pizzas", async (PizzaContext db) => await db.Pizzas.ToListAsync());

app.MapGet("/pizza/{id}", async (PizzaContext db, int id) => await db.Pizzas.FindAsync(id));

app.MapPost("/pizza", async (PizzaContext db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

app.MapPut("/pizza/{id}", async (PizzaContext db, Pizza updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();

    if (!string.IsNullOrEmpty(updatepizza.Name))
    {
        pizza.Name = updatepizza.Name;
    }

    if (!string.IsNullOrEmpty(updatepizza.Description))
    {
        pizza.Description = updatepizza.Description;
    }

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pizza/{id}", async (PizzaContext db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
    {
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});
#endregion

app.Run();
