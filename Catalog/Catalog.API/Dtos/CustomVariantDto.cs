using Catalog.Domain.Models;

namespace Catalog.API.Dtos;

public class CustomVariantDto
{
    public CustomVariantDto()
    {
    }

    public CustomVariantDto(CustomVariant customVariant)
    {
        Id = customVariant.Id;
        BasePrice = customVariant.PricePolicy.BasePrice;
        CustomVariantType = customVariant.CustomVariantType;
    }
    
    public int Id { get; set; }
    public decimal BasePrice { get; set; }
    public string CustomVariantType { get; set; }
}