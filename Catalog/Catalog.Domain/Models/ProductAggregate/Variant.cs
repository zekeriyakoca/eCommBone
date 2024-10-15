using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Models;

public class Variant : Entity
{
    public int StockQuantity { get; set; }
    public int StockThreshold { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
    public int Order { get; set; }
    
    public int ProductId { get; set; }
    
    public ICollection<Image> Images { get; set; }
    public ICollection<ProductAttribute> Attributes { get; set; }
}