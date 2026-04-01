using System.ComponentModel.DataAnnotations.Schema;

namespace GrapheneCore.Entities;
public class Order : Entity
{
    public string Tag { get; set; } = string.Empty;

    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

    public Product? Product { get; set; }

    [ForeignKey(nameof(Product))]
    public virtual long ProductId { get; set; }
}
