using System.Text.Json;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;

public static class DataSeeder
{
    public static async Task Seed(CatalogDbContext context)
    {
        try
        {
            await context.Database.BeginTransactionAsync();
            
            await context.Database.EnsureCreatedAsync();

            // context.Categories.ExecuteDelete();
            // context.Products.ExecuteDelete();
            // context.CustomVariants.ExecuteDelete();

            if (context.Categories.Any()) return;

            var category = new Category("Electronics", "Electronic Items");

            context.Categories.Add(category);

            await context.SaveChangesAsync();
        
            var sampleProduct = GetSampleProduct(category);

            context.Add(sampleProduct);
            // context.Add(CloneProductWith(sampleProduct, "Alienware m16 R3 gaminglaptop", "Alienware m16 R3 gaminglaptop", "Alienware m16 R3 gaminglaptop", 1200));
            // context.Add(CloneProductWith(sampleProduct, "Alienware m14 R3 gaminglaptop", "Alienware m14 R3 gaminglaptop", "Alienware m14 R3 gaminglaptop", 1033));
            // context.Add(CloneProductWith(sampleProduct, "Alienware m15 R3 gaminglaptop", "Alienware m15 R3 gaminglaptop", "Alienware m15 R3 gaminglaptop", 1140));

            await context.SaveChangesAsync();
            
            await context.Database.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await context.Database.RollbackTransactionAsync();
            throw;
        }
    }

    private static Product GetSampleProduct(Category category)
    {
        var product = new Product("Alienware m18 R2 gaminglaptop", "Alienware m18 R2 gaminglaptop", "Alienware m18 R2 gaminglaptop", "DELL Technologies", "DELL", category.Id,
            ProductTags.Featured, 1420, 100, 10, 0, 12,
            new List<string>()
            {
                "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/alienware-notebooks/alienware-m18-mlk/media-gallery/hd/laptop-alienware-m18-r2-hd-perkey-intel-bk-gallery-2.psd?fmt=png-alpha&pscan=auto&scl=1&hei=402&wid=522&qlt=100,1&resMode=sharp2&size=522,402&chrss=full"
            });

        product.Variants.First().Attributes.Add(new ProductAttribute()
        {
            Name = "Intel Core i7",
            Description = "Intel Core i7",
            CustomValue = "Intel Core i7",
            Group = new AttributeGroup()
            {
                Name = "CPU",
                Description = "CPU"
            }
        });
        product.Variants.First().Attributes.Add(
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
            });
        return product;
    }

    private static Product CloneProductWith(Product refProduct, string name, string desc, string title, int price)
    {
        var newProduct = JsonSerializer.Deserialize<Product>(JsonSerializer.Serialize(refProduct, new JsonSerializerOptions() { IncludeFields = true }), new JsonSerializerOptions() { IncludeFields = false });
        if (newProduct == null) throw new Exception("Failed to clone product");
        newProduct.Name = name;
        newProduct.Description = desc;
        newProduct.Title = title;
        newProduct.Variants.First().Price = price;
        return newProduct;
    }
}