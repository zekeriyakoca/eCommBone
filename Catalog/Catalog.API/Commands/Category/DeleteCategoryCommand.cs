using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly CatalogDbContext _context;

    public DeleteCategoryCommandHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deletedItemsCount = await _context.Categories.Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);
        return deletedItemsCount > 0;
    }
}