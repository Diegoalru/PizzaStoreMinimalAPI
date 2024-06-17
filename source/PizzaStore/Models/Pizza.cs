using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models;

[Table("Pizzas")]
[Index(nameof(Name), IsUnique = true)]
public class Pizza
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required]
    [StringLength(100)]
    public required string Description { get; set; }
}
