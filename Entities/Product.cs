using System.ComponentModel.DataAnnotations.Schema;

namespace GrapheneCore.Entities;
public class Product : Entity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual IEnumerable<Order>? Orders { get; set; }
}
