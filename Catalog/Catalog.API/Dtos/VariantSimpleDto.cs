using Catalog.Domain.Models;
using Catalog.Domain.Utils;

namespace Catalog.API.Dtos;

public class VariantSimpleDto
{
    public VariantSimpleDto()
    {
        
    }

    public VariantSimpleDto(Variant v)
    {
        Id = v.Id;
        Name = v.Name;
        Price = PriceManager.CalculatePrice(v.Price, v.DiscountRate, v.DiscountAmount);
        DiscountRate = v.DiscountRate;
        DiscountAmount = v.DiscountAmount;
        Image = v.Images.Select(i => new ImageDto(i)).FirstOrDefault();
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
    public ImageDto Image { get; set; }
}