using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Queries;

public record DeleteCatalogQuery(int Id) : IRequest<bool>;

public class DeleteCatalogQueryHandler : IRequestHandler<DeleteCatalogQuery, bool>
{
    private readonly CatalogDbContext _context;

    public DeleteCatalogQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCatalogQuery request, CancellationToken cancellationToken)
    {
        var deletedItemsCount = await _context.Categories.Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return deletedItemsCount > 0;
    }
}