using Catalog.API.Dtos;
using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Queries;

public record SearchProductQuery(string? SearchTerm,
    int? CategoryId,
    int? PriceFrom,
    int? PriceTo,
    ProductTags? Tags,
    bool IsOnlyAvailableItems = true
) : PaginationQuery, IRequest<PaginatedItems<ProductDto>>;

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PaginatedItems<ProductDto>>
{
    private readonly CatalogDbContext _context;

    public SearchProductQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedItems<ProductDto>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products
            .Include(x=>x.Variants)
            .Include(x=>x.CustomVariant)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            // TODO : Implement free text search and, then, search utilizing AI
            query = query.Where(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm) || x.Owner.Contains(request.SearchTerm));
        }

        if (request.PriceFrom.HasValue)
        {
            query = query.Where(x => x.GetLowestPrice() >= request.PriceFrom);
        }

        if (request.PriceTo.HasValue)
        {
            query = query.Where(x => x.GetLowestPrice() <= request.PriceTo);
        }

        if (request.IsOnlyAvailableItems)
        {
            query = query.Where(x => x.AvailableStock > 0);
        }

        if (request.Tags.HasValue)
        {
            query = query.Where(x => x.Tags.HasValue && x.Tags.Value.HasFlag(request.Tags.Value));
        }

        if (request.CategoryId.HasValue)
        {
            // TODO : Fix here to search for all subcategories
            query = query.Where(x => (x.CategoryId == request.CategoryId));
        }

        var count = await query.LongCountAsync(cancellationToken);
        var items = await query
            .OrderBy(x => x.Id)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProductDto(x))
            .ToListAsync(cancellationToken);

        return new PaginatedItems<ProductDto>(request.PageIndex, request.PageSize, count, items);
    }
}