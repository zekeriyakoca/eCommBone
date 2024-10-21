using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Models;

public class Variant : Entity
{
    public Variant()
    {
        Images = new List<Image>();
        Attributes = new List<ProductAttribute>();
    }
    public Variant(string name, string description, int stockQuantity, int stockThreshold, decimal price, decimal discountRate, decimal discountAmount, int order = 1,
        List<Image>? images = null, List<ProductAttribute>? attributes = null)
    {
        Name = name;
        Description = description;
        StockQuantity = stockQuantity;
        StockThreshold = stockThreshold;
        Price = price;
        DiscountRate = discountRate;
        DiscountAmount = discountAmount;
        Order = int.Max(order, 1);
        Images = images ?? new List<Image>();
        Attributes = attributes ?? new List<ProductAttribute>();
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public int StockThreshold { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
    public int Order { get; set; }

    public int ProductId { get; set; }

    public ICollection<Image> Images { get; set; }
    public ICollection<ProductAttribute> Attributes { get; set; }
}