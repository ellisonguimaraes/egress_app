using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(p => p.BirthDate)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .EmailAddress()
                .WithMessage(string.Format(ValidationResource.VALIDATION_CONTAINS_UNSUPPORTED_FORMAT, "{PropertyName}", ". Try format email@provider.com."));

        RuleFor(p => p.PhoneNumber)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(p => p.CanExposeData)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(p => p.CanReceiveMessage)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
        
        RuleFor(p => p.PersonType)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .IsInEnum().WithMessage(ValidationResource.VALIDATION_IS_INVALID);

        RuleFor(p => p.Address)
            .NotNull().WithMessage(ValidationResource.VALIDATION_NOT_NULL);
        
        RuleFor(p => p.Employment)
            .NotNull().WithMessage(ValidationResource.VALIDATION_NOT_NULL);
        
        RuleFor(p => p.ContinuingEducation)
            .NotNull().WithMessage(ValidationResource.VALIDATION_NOT_NULL);

        RuleFor(p => p.Address)
            .SetValidator(new AddressEntryModelValidator()!)
                .When(p => p.Address is not null);

        RuleFor(p => p.Employment)
            .SetValidator(new EmploymentEntryModelValidator()!)
            .When(p => p.Employment is not null);
        
        RuleFor(p => p.ContinuingEducation)
            .SetValidator(new ContinuingEducationEntryModelValidator()!)
            .When(p => p.ContinuingEducation is not null);
        
        RuleFor(u => u.Id)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    }
}