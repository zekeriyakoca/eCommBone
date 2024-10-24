using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Catalog.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Models;

public class Product : Entity, IAggregateRoot
{
    [Required] [MaxLength(50)] public string Name { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Owner { get; set; }

    public string Brand { get; set; }

    public int? CustomVariantId { get; set; }
    public CustomVariant? CustomVariant { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ProductTags? Tags { get; set; }

    public int SoldCount { get; set; }

    // Quantity in stock
    public int AvailableStock { get; set; }

    // Available stock at which we should reorder
    public int RestockThreshold { get; set; }

    // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
    public int MaxStockThreshold { get; set; }

    /// <summary>
    /// True if item is on reorder
    /// </summary>
    public bool OnReorder { get; set; }

    private List<Variant> _variants = new();

    [JsonIgnore]
    [BackingField("_variants")]
    public IReadOnlyCollection<Variant> Variants => _variants.AsReadOnly();

    public void AddVariant(Variant variant)
    {
        if (_variants.Any(v => v.Name == variant.Name))
        {
            throw new CatalogDomainException($"Variant with name {variant.Name} already exists.");
        }

        _variants.Add(variant);
    }

    public void RemoveVariant(int variantId)
    {
        var variantIndex = _variants.FindIndex(p => p.Id == variantId);
        if (variantIndex > 0)
            _variants.RemoveAt(variantIndex);
    }


    public ICollection<Image> Images { get; set; } = new List<Image>();

    // Related products (as children)
    public virtual ICollection<ProductRelation> ChildRelations { get; set; } = new List<ProductRelation>();

    // Related products (as parents)
    public virtual ICollection<ProductRelation> ParentRelations { get; set; } = new List<ProductRelation>();

    // Products that are related to this product, either as parent or child
    public IEnumerable<Product> RelatedProducts
    {
        get { return ChildRelations.Select(r => r.ChildProduct).Distinct(); }
    }

    private Product()
    {
    }

    public Product(string name, string description, string title, string owner, string brand, int categoryId, ProductTags? tags, decimal price, int stockQuantity,
        int stockThreshold, decimal discountAmount, decimal discountRate, List<string> imageUrls)
    {
        Name = name;
        Description = description;
        Title = title;
        Owner = owner;
        Brand = brand;
        CategoryId = categoryId;
        Tags = tags;
        AddVariant(new Variant(name, description, stockQuantity, stockThreshold, price, discountRate, discountAmount));
        imageUrls.ForEach(x => Images.Add(new Image { Original = x, Name = "Product image" }));
    }

    /// <summary>
    /// Decrements the quantity of a particular item in inventory and ensures the restockThreshold hasn't
    /// been breached. If so, a RestockRequest is generated in CheckThreshold. 
    /// 
    /// If there is sufficient stock of an item, then the integer returned at the end of this call should be the same as quantityDesired. 
    /// In the event that there is not sufficient stock available, the method will remove whatever stock is available and return that quantity to the client.
    /// In this case, it is the responsibility of the client to determine if the amount that is returned is the same as quantityDesired.
    /// It is invalid to pass in a negative number. 
    /// </summary>
    /// <param name="quantityDesired"></param>
    /// <returns>int: Returns the number actually removed from stock. </returns>
    /// 
    public int RemoveStock(int quantityDesired)
    {
        if (AvailableStock == 0)
        {
            throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
        }

        if (quantityDesired <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }

        int removed = Math.Min(quantityDesired, this.AvailableStock);

        this.AvailableStock -= removed;

        return removed;
    }

    /// <summary>
    /// Increments the quantity of a particular item in inventory.
    /// <param name="quantity"></param>
    /// <returns>int: Returns the quantity that has been added to stock</returns>
    /// </summary>
    public int AddStock(int quantity)
    {
        int original = this.AvailableStock;

        // The quantity that the client is trying to add to stock is greater than what can be physically accommodated in the Warehouse
        if ((this.AvailableStock + quantity) > this.MaxStockThreshold)
        {
            // For now, this method only adds new units up maximum stock threshold. In an expanded version of this application, we
            //could include tracking for the remaining units and store information about overstock elsewhere. 
            this.AvailableStock += (this.MaxStockThreshold - this.AvailableStock);
        }
        else
        {
            this.AvailableStock += quantity;
        }

        this.OnReorder = false;

        return this.AvailableStock - original;
    }

    public virtual decimal GetLowestPrice()
    {
        if (CustomVariant != null)
        {
            return CustomVariant.GetCalculatedPrice();
        }

        return Variants.Min(x => x.GetCalculatedPrice());
    }
}