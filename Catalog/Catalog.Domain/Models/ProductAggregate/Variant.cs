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
    
    public IEnumerable<Image> Images { get; set; }
    public IEnumerable<ProductAttribute> Attributes { get; set; }
}