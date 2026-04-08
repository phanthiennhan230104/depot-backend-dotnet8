using CleanArchitecture.Shared.Models.Requests.Yards.Positions;
using FluentValidation;

namespace CleanArchitecture.Web.Validations.Yards.Positions;

public class UpdatePositionRequestValidator : AbstractValidator<UpdatePositionRequest>
{
    public UpdatePositionRequestValidator()
    {
        RuleFor(x => x.PositionCode)
            .NotEmpty().WithMessage("Position code is required.")
            .MaximumLength(50);

        RuleFor(x => x.BayNo)
            .GreaterThan(0)
            .When(x => x.BayNo.HasValue);

        RuleFor(x => x.RowNo)
            .GreaterThan(0)
            .When(x => x.RowNo.HasValue);

        RuleFor(x => x.TierNo)
            .GreaterThan(0)
            .When(x => x.TierNo.HasValue);
    }
}
