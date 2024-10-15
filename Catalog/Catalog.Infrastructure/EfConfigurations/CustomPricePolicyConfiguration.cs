using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EfConfigurations;

public class CustomPricePolicyConfiguration : IEntityTypeConfiguration<CustomPricePolicy>
{
    public void Configure(EntityTypeBuilder<CustomPricePolicy> builder)
    {
        builder.ToTable("PricePolicies"); // Table name for TPH

        // Primary key
        builder.HasKey(p => p.Id);

        // Discriminator configuration for TPH
        builder.HasDiscriminator<string>("PricePolicyType")
            .HasValue<SquareBasePricePolicy>("Square");
    }
}