using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class AddressEntryModelValidator : AbstractValidator<AddressEntryModel>
{
    public AddressEntryModelValidator()
    {
        RuleFor(a => a.State)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(a => a.Country)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(a => a.IsPublic)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    }
}
