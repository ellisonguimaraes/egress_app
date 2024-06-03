using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class RegisterPersonCommandValidator : AbstractValidator<RegisterPersonCommand>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    private const string REGEX_CPF_MATCH = @"^\d{11}$";
    private const string CPF_ERROR_MESSAGE_COMPLETING = ". Contains 11 digits and do not use special characters.";
    #endregion

    public RegisterPersonCommandValidator()
    {
        RuleFor(p => p.Cpf)
            .Matches(REGEX_CPF_MATCH).WithMessage(string.Format(ValidationResource.VALIDATION_INVALID_FORMAT, PROPERTY_NAME, CPF_ERROR_MESSAGE_COMPLETING))
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

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
    }
}
