using Catalog.Domain.Models;

namespace Catalog.API.Dtos;

public class ProductAttributeDto
{
    public ProductAttributeDto()
    {
    }

    public ProductAttributeDto(ProductAttribute attribute)
    {
        Name = attribute.Name;
        Description = attribute.Description;
        CustomValue = attribute.CustomValue;
        GroupName = attribute.Group?.Name;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string CustomValue { get; set; }
    public string GroupName { get; set; }
}