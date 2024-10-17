using Catalog.Domain.Exceptions;
using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Commands;

public record AddCategoryCommand(string Name, string Description, int? ParentCategoryId) : IRequest<int>;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
{
    private readonly CatalogDbContext _context;
    public AddCategoryCommandHandler(CatalogDbContext context)
    {
        this._context = context;
    }
    
    public async Task<int> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentCategoryId.HasValue && !await _context.Categories.AnyAsync(x=>x.Id == request.ParentCategoryId, cancellationToken))
        {
            throw new CatalogDomainException("Parent category not found!");
        }

        var category = new Category()
        {
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId
        };
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);
        
        return category.Id;
        
    }
}