using System.Text.Json;
using Catalog.Domain.Models;

namespace Catalog.Infrastructure;

public static class DataSeeder
{
    public static async Task Seed(CatalogDbContext context)
    {
        if(context.Categories.Any()) return;
        
        var product1 = new Product()
        {
            Name = "Alienware m18 R2 gaminglaptop",
            Description = "Alienware m18 R2 gaminglaptop",
            Title = "Alienware m18 R2 gaminglaptop",
            Price = 1420,
            AvailableStock = 100,
            RestockThreshold = 10,
            MaxStockThreshold = 100,
            OnReorder = false,
            Variants = new List<Variant>()
            {
                new Variant()
                {
                    Order = 0,
                    DiscountAmount = 0,
                    DiscountRate = 12,
                    Name = "Intel Core i7__RTX 4060",
                    Description = "Intel Core i7 / RTX 4060",
                    Attributes = new List<ProductAttribute>()
                    {
                        new ProductAttribute()
                        {
                            Name = "Intel Core i7",
                            Description = "Intel Core i7",
                            CustomValue = "Intel Core i7",
                            Group = new AttributeGroup()
                            {
                                Name = "CPU",
                                Description = "CPU"
                            }
                        },
                        new ProductAttribute()
                        {
                            Name = "GeForce RTX 4060",
                            Description = "NVIDIA GeForce RTX 4060",
                            CustomValue = "NVIDIA GeForce RTX 4060",
                            Group = new AttributeGroup()
                            {
                                Name = "GPU",
                                Description = "Graphics Processing Unit"
                            }
                        }
                    }
                }
            },
            Images = new List<Image>()
            {
                new Image()
                {
                    Order = 1,
                    Name = "m18 R2",
                    Original = 
                        "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/alienware-notebooks/alienware-m18-mlk/media-gallery/hd/laptop-alienware-m18-r2-hd-perkey-intel-bk-gallery-2.psd?fmt=png-alpha&pscan=auto&scl=1&hei=402&wid=522&qlt=100,1&resMode=sharp2&size=522,402&chrss=full",
                    Thumb =
                        "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/alienware-notebooks/alienware-m18-mlk/media-gallery/hd/laptop-alienware-m18-r2-hd-perkey-intel-bk-gallery-2.psd?fmt=png-alpha&pscan=auto&scl=1&hei=402&wid=522&qlt=100,1&resMode=sharp2&size=522,402&chrss=full",
                    Small =
                        "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/alienware-notebooks/alienware-m18-mlk/media-gallery/hd/laptop-alienware-m18-r2-hd-perkey-intel-bk-gallery-2.psd?fmt=png-alpha&pscan=auto&scl=1&hei=402&wid=522&qlt=100,1&resMode=sharp2&size=522,402&chrss=full",
                    Medium =
                        "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/alienware-notebooks/alienware-m18-mlk/media-gallery/hd/laptop-alienware-m18-r2-hd-perkey-intel-bk-gallery-2.psd?fmt=png-alpha&pscan=auto&scl=1&hei=402&wid=522&qlt=100,1&resMode=sharp2&size=522,402&chrss=full",
                    Large =
                        "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/alienware-notebooks/alienware-m18-mlk/media-gallery/hd/laptop-alienware-m18-r2-hd-perkey-intel-bk-gallery-2.psd?fmt=png-alpha&pscan=auto&scl=1&hei=402&wid=522&qlt=100,1&resMode=sharp2&size=522,402&chrss=full",
                }
            },
            Owner = "DELL Technologies",
            Brand = "DELL",
            Tags = ProductTags.Featured,
        };
        
        var category = new Category()
        {
            Name = "Electronics",
            Description = "Electronic Items",
            Slug = "electronics",
            Products = new List<Product>()
            {
                product1,
                CloneProductWith(product1, "Alienware m16 R3 gaminglaptop", "Alienware m16 R3 gaminglaptop", "Alienware m16 R3 gaminglaptop", 1200),
                CloneProductWith(product1, "Alienware m14 R3 gaminglaptop", "Alienware m14 R3 gaminglaptop", "Alienware m14 R3 gaminglaptop", 1033),
                CloneProductWith(product1, "Alienware m15 R3 gaminglaptop", "Alienware m15 R3 gaminglaptop", "Alienware m15 R3 gaminglaptop", 1140),
            }
        };
        
        context.Categories.Add(category);
        
        await context.SaveChangesAsync();
    }

    private static Product CloneProductWith(Product refProduct, string name, string desc, string title, int price)
    {
        var newProduct = JsonSerializer.Deserialize<Product>(JsonSerializer.Serialize(refProduct));
        if(newProduct == null) throw new Exception("Failed to clone product");
        newProduct.Name = name;
        newProduct.Description = desc;
        newProduct.Title = title;
        newProduct.Price = price;
        return newProduct;
    }
}