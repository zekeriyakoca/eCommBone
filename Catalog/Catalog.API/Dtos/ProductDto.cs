using Catalog.Domain.Models;

namespace Catalog.API.Dtos;

public class ProductDto
{
    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Category = new SimpleCategoryDto(product.Category);
        Images = product.Images.Select(i => new ImageDto(i));
        HasCustomVariant = product.CustomVariantId > 0;
        CustomVariant = product.CustomVariant != default ? new CustomVariantDto(product.CustomVariant) : default;
        Variants = product.Variants.Select(v => new VariantDto(v));
        Price = product.CustomVariantId > 0 ? 0 : this.Variants.Min(v => v.Price);
        DiscountRate = product.CustomVariant != default ? product.CustomVariant.DiscountRate : this.Variants.Min(v => v.DiscountRate);
        DiscountAmount = product.CustomVariant != default ? product.CustomVariant.DiscountAmount : this.Variants.Min(v => v.DiscountAmount);
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SimpleCategoryDto Category { get; set; }
    public IEnumerable<ImageDto> Images { get; set; }
    public bool HasCustomVariant { get; set; }
    public CustomVariantDto? CustomVariant { get; set; }
    public IEnumerable<VariantDto> Variants { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
}