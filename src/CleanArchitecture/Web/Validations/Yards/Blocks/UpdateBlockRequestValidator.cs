using CleanArchitecture.Shared.Models.Requests.Yards.Blocks;
using FluentValidation;

namespace CleanArchitecture.Web.Validations.Yards.Blocks;

public class UpdateBlockRequestValidator : AbstractValidator<UpdateBlockRequest>
{
    public UpdateBlockRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Block code is required.")
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Block name is required.")
            .MaximumLength(255);

    }
}
