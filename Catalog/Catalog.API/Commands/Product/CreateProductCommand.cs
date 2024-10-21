using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Events;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Commands;

public record CreateProductCommand(
    [Required] string Name,
    [Required] string Description,
    [Required] string Title,
    string Owner,
    string Brand,
    [Range(1, int.MaxValue)][Required] int CategoryId,
    ProductTags Tags,
    // CustomVariant? CustomVariant, // Temporarily disabled
    List<string> Images,
    [Required] decimal Price,
    int StockQuantity = 100,
    int StockThreshold = 10,
    decimal DiscountRate = 0,
    decimal DiscountAmount = 0
) : IRequest<int>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly CatalogDbContext _context;

    public CreateProductCommandHandler(CatalogDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken))
        {
            throw new CatalogDomainException("Category not found!");
        }

        var product = new Product(request.Name, request.Description, request.Title, request.Owner, request.Brand, request.CategoryId, request.Tags, request.Price, request.StockQuantity, request.StockThreshold, request.DiscountAmount, request.DiscountRate, request.Images);
        
        product.AddDomainEvent(new ProductCreatedDomainEvent());
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}