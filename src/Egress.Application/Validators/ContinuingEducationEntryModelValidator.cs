using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class ContinuingEducationEntryModelValidator : AbstractValidator<ContinuingEducationEntryModel>
{
    public ContinuingEducationEntryModelValidator()
    {
        RuleFor(c => c.HasCertification)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(c => c.HasSpecialization)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(c => c.HasMasterDegree)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(c => c.HasDoctorateDegree)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(c => c.IsPublic)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    }
}