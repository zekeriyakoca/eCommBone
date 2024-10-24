using Catalog.API.Commands;
using FluentValidation;

namespace Catalog.API.Validations;

// We're already using Validation Annotations in the CreateCategoryCommand record, so we don't need to add any more validations here. 
// This class is just a placeholder for future validations.
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        // RuleFor(x => x.Name).NotEmpty();
        // RuleFor(x => x.Description).NotEmpty();
        // RuleFor(x => x.ParentCategoryId)
        //     .Must(value => value == null || (value >= 1 && value <= int.MaxValue))
        //     .WithMessage("Value must be null or between 1 and int.MaxValue.");
    }
}