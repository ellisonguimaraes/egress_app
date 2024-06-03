using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class EmploymentEntryModelValidator : AbstractValidator<EmploymentEntryModel>
{
    public EmploymentEntryModelValidator()
    {
        RuleFor(e => e.Role)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(e => e.Enterprise)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    
        RuleFor(p => p.IsPublicInitiative)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(p => p.IsPublic)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(p => p.StartDate)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    }
}
