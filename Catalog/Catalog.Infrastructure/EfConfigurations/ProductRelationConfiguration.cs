using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EfConfigurations;

public class ProductRelationConfiguration : IEntityTypeConfiguration<ProductRelation>
{
    public void Configure(EntityTypeBuilder<ProductRelation> builder)
    {
        // Define the primary key
        builder.HasKey(pr => new { pr.ChildProductId, pr.ParentProductId });

        builder.HasOne(pr => pr.ChildProduct)
            .WithMany(p => p.ChildRelations)
            .HasForeignKey(pr => pr.ChildProductId);

        builder.HasOne(pr => pr.ParentProduct)
            .WithMany(p => p.ParentRelations)
            .HasForeignKey(pr => pr.ParentProductId);
    }
}