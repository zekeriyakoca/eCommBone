using Catalog.API.Dtos;
using Catalog.Domain.Models;
using Catalog.Domain.Utils;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly CatalogDbContext _context;

    public GetProductByIdQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Images)
            .Include(x => x.CustomVariant).ThenInclude(x => x.PricePolicy)
            .Include(x => x.Variants).ThenInclude(x => x.Attributes).ThenInclude(x=>x.Group)
            .Include(x => x.Variants).ThenInclude(x => x.Images)
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }
}