namespace Catalog.Domain.Models;

public class Category : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    
    public int ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }
}