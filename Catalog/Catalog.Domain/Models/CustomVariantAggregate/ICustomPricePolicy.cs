using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Models;

[Table("PricePolicies")]
public abstract class CustomPricePolicy : Entity
{
    public abstract decimal Price { get; }
    public decimal BasePrice { get; set; }
    public virtual string Spec1 { get; set; }
    public virtual string Spec2 { get; set; }
    public virtual string Spec3 { get; set; }
    public virtual string Spec4 { get; set; }
}