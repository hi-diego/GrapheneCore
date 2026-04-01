using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GrapheneCore.Entities;

public class BaseEntity
{
    [Key]
    [JsonIgnore]
    public virtual long Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Uuid { get; set; } = Guid.NewGuid();

    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual DateTime? ModifiedAt { get; set; }
}

public class Entity : BaseEntity
{
}
