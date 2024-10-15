using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EfConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasMany(p => p.ChildRelations)
            .WithOne(pr => pr.ChildProduct)
            .HasForeignKey(pr => pr.ChildProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.ParentRelations)
            .WithOne(pr => pr.ParentProduct)
            .HasForeignKey(pr => pr.ParentProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}