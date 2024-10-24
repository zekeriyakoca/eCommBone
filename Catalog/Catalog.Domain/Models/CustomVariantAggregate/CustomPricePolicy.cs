using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Models;

[Table("PricePolicies")]
public abstract class CustomPricePolicy : Entity
{
    public abstract decimal Price { get; }
    [Required]
    public decimal BasePrice { get; set; }
    [MaxLength(50)]
    public virtual string Spec1 { get; set; }
    [MaxLength(50)]
    public virtual string Spec2 { get; set; }
    [MaxLength(50)]
    public virtual string Spec3 { get; set; }
    [MaxLength(50)]
    public virtual string Spec4 { get; set; }
}