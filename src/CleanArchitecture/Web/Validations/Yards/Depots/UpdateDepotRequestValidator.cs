using CleanArchitecture.Shared.Models.Requests.Yards.Depots;
using FluentValidation;

namespace CleanArchitecture.Web.Validations.Yards.Depots;

public class UpdateDepotRequestValidator : AbstractValidator<UpdateDepotRequest>
{
    public UpdateDepotRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Depot code is required.")
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Depot name is required.")
            .MaximumLength(255);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Depot address is required.")
            .MaximumLength(255);
        
    }
}
