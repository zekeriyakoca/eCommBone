using Catalog.Domain.Models;
using Catalog.Domain.Utils;

namespace Catalog.API.Dtos;

public class VariantDto
{
    public VariantDto()
    {
        
    }

    public VariantDto(Variant v)
    {
        Id = v.Id;
        Name = v.Name;
        Price = PriceManager.CalculatePrice(v.Price, v.DiscountRate, v.DiscountAmount);
        DiscountRate = v.DiscountRate;
        DiscountAmount = v.DiscountAmount;
        Attributes = v.Attributes.Select(a => new ProductAttributeDto(a));
        Images = v.Images.Select(i => new ImageDto(i));
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
    public IEnumerable<ProductAttributeDto> Attributes { get; set; }
    public IEnumerable<ImageDto> Images { get; set; }
}