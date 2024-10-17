using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Models;

public class Category : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }

    [ForeignKey("ParentCategoryId")]
    public Category ParentCategory { get; set; }
    public int? ParentCategoryId { get; set; }
    
    public ICollection<Image> Images { get; set; } = new List<Image>();
}