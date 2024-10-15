namespace Catalog.Domain.Models;

public class Image : Entity
{
    public string Name { get; set; }
    public string Thumb { get; set; }
    public string Small { get; set; }
    public string Medium { get; set; }
    public string Large { get; set; }
    public string Original { get; set; }
    public int Order { get; set; }
    
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int? VariantId { get; set; }
    public Variant? Variant { get; set; }
    
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}