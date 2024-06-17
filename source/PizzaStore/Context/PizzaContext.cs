using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Context;

public class PizzaContext(DbContextOptions<PizzaContext> options) : DbContext(options)
{
    public required DbSet<Pizza> Pizzas { get; set; }
}
