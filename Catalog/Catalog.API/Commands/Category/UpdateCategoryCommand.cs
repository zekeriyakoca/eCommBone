using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Models;
using Catalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Commands;

public record UpdateCategoryCommand(
    [Required, Range(1, int.MaxValue)] int Id,
    [Required] string Name,
    [Required] string Description,
    [Range(1, int.MaxValue)] int? ParentCategoryId
) : IRequest<Category>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly CatalogDbContext _context;

    public UpdateCategoryCommandHandler(CatalogDbContext context)
    {
        this._context = context;
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
        if (category == null)
        {
            throw new NotFoundException("category not found!");
        }

        if (request.ParentCategoryId.HasValue && !await _context.Categories.AnyAsync(x => x.Id == request.ParentCategoryId, cancellationToken))
        {
            throw new CatalogDomainException("Parent category not found!");
        }

        category.ChangeParentCategoryId(request.ParentCategoryId);
        category.ChangeName(request.Name);
        category.ChangeDescription(request.Description);
        
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }
}