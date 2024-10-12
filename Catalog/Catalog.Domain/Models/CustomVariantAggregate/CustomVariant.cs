namespace Catalog.Domain.Models;

public class CustomVariant : Entity
{
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
    public string CustomVariantType { get; set; }
    
    public int PricePolicyId { get; set; }
    public CustomPricePolicy PricePolicy { get; set; }

}