using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Catalog.Domain.Utils;

namespace Catalog.Domain.Models;

public class Category : Entity, IAggregateRoot
{
    public Category(string name, string description, int? parentCategoryId = default)
    {
        Name = name;
        Description = description;
        Slug = description.GenerateSlug();
        ParentCategoryId = parentCategoryId;
    }
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }

    public string Slug { get; private set; }

    [ForeignKey("ParentCategoryId")]
    public Category ParentCategory { get; set; }
    public int? ParentCategoryId { get; private set; }
    
    public ICollection<Image> Images { get; private set; } = new List<Image>();
    
    public void ChangeName(string name)
    {
        Name = name;
    }
    
    public void ChangeDescription(string name)
    {
        Name = name;
        Slug = name.GenerateSlug();
    }
    
    public void ChangeParentCategoryId(int? parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
    }
    
    public void ChangeImages(ICollection<Image> images)
    {
        Images = images;
    }
    
    public void AddImages(ICollection<Image> newImages)
    {
        foreach (var image in newImages)
        {
            Images.Add(image);
        }
    }
}