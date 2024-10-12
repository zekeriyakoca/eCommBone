namespace Catalog.Domain.Models;

public class ProductRelation : Entity
{
    public int IsJointProduct { get; set; }
    public ProductRelationType Type { get; set; }
    
    public int ChildProductId { get; set; }
    public Product ChildProduct { get; set; }
    
    public int ParentProductId { get; set; }
    public Product ParentProduct { get; set; }
}

[Flags]
public enum ProductRelationType
{
    RelatedItem = 1 << 0,
    CrossSell = 1 << 1,
    UpSell = 1 << 2
}