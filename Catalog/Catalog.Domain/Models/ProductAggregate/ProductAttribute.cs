namespace Catalog.Domain.Models;

public class ProductAttribute : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string CustomValue { get; set; }
    
    public int ProductId { get; set; }
    
    public int GroupId { get; set; }
    public AttributeGroup Group { get; set; }
}