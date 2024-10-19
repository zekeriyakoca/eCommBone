using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<Category?>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    private readonly CatalogDbContext _context;

    public GetCategoryByIdQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .Include(x=>x.ParentCategory.ParentCategory)
            .Where(x=> x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}