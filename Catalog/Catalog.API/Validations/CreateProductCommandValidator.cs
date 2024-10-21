using Catalog.API.Commands;
using FluentValidation;

namespace Catalog.API.Validations;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => new { StockQuantity = x.StockQuantity, StockThreshold = x.StockThreshold })
            .Must(x => x.StockQuantity >= 0 && x.StockThreshold >= 0 && x.StockQuantity >= x.StockThreshold)
            .WithMessage("StockQuantity and StockThreshold must be greater than or equal to 0 and StockQuantity must be greater than or equal to StockThreshold.");
    }
}