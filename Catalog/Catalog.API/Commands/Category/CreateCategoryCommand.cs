using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Events;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Commands;

public record CreateCategoryCommand(
    [Required] string Name,
    [Required] string Description,
    [Range(1, int.MaxValue)] int? ParentCategoryId
) : IRequest<int>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly CatalogDbContext _context;

    public CreateCategoryCommandHandler(CatalogDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentCategoryId.HasValue && !await _context.Categories.AnyAsync(x => x.Id == request.ParentCategoryId, cancellationToken))
        {
            throw new CatalogDomainException("Parent category not found!");
        }

        var category = new Category(request.Name, request.Description, request.ParentCategoryId);
        category.AddDomainEvent(new CategoryCreatedDomainEvent());
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}