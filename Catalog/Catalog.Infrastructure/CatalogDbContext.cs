using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure;

public class CatalogDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Product> Products { get; set; }
    public DbSet<CustomVariant> CustomVariants { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    public CatalogDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["SqlConnection"]);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}