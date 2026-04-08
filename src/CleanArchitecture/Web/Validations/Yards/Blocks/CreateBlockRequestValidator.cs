using CleanArchitecture.Shared.Models.Requests.Yards.Blocks;
using FluentValidation;

namespace CleanArchitecture.Web.Validations.Yards.Blocks;

public class CreateBlockRequestValidator : AbstractValidator<CreateBlockRequest>
{
    public CreateBlockRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Block code is required.")
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Block name is required.")
            .MaximumLength(255);

        RuleFor(x => x.DepotId)
            .NotEmpty().WithMessage("DepotId is required.");

    }
}
