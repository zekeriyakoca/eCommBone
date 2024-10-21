using Catalog.Domain.Models;

namespace Catalog.API.Dtos;

public class SimpleCategoryDto
{
    public SimpleCategoryDto()
    {
    }

    public SimpleCategoryDto(Category category)
    {
        Name = category.Name;
        Description = category.Description;
        Slug = category.Slug;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
}