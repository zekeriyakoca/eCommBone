using Catalog.Domain.Models;

namespace Catalog.API.Dtos;

public class ProductSimpleDto
{
    public ProductSimpleDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Image = product.Images.Select(i => new ImageDto(i)).FirstOrDefault();
        HasCustomVariant = product.CustomVariantId > 0;
        CustomVariant = product.CustomVariant != default ? new CustomVariantDto(product.CustomVariant) : default;
        Variants = product.Variants.Select(v => new VariantSimpleDto(v)).ToList();
        Price = product.CustomVariantId > 0 ? 0 : Variants.Min(v => v.Price);
        DiscountRate = product.CustomVariant != default ? product.CustomVariant.DiscountRate : this.Variants.Min(v => v.DiscountRate);
        DiscountAmount = product.CustomVariant != default ? product.CustomVariant.DiscountAmount : this.Variants.Min(v => v.DiscountAmount);
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageDto? Image { get; set; }
    public bool HasCustomVariant { get; set; }
    public CustomVariantDto? CustomVariant { get; set; }
    public IEnumerable<VariantSimpleDto> Variants { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
}