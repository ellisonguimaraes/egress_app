using Egress.Application.Commands.Person.CreateBasicPerson;
using Egress.Domain.Enums;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class CreateBasicPersonCommandValidator : AbstractValidator<CreateBasicPersonCommand>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    private const string REGEX_CPF_MATCH = @"^\d{11}$";
    private const string CPF_ERROR_MESSAGE_COMPLETING = ". Contains 11 digits and do not use special characters.";
    #endregion

    public CreateBasicPersonCommandValidator()
    {
        RuleFor(p => p.Cpf)
            .Matches(REGEX_CPF_MATCH).WithMessage(string.Format(ValidationResource.VALIDATION_INVALID_FORMAT, PROPERTY_NAME, CPF_ERROR_MESSAGE_COMPLETING))
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(p => p.Email)
            .EmailAddress()
                .WithMessage(string.Format(ValidationResource.VALIDATION_CONTAINS_UNSUPPORTED_FORMAT, "{PropertyName}", ". Try format email@provider.com."));

        RuleFor(p => p.PersonType)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .IsInEnum().WithMessage(ValidationResource.VALIDATION_IS_INVALID);

        RuleFor(p => p.Course)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
                .When(p => p.PersonType.Equals(PersonType.EGRESS));
        
        RuleFor(p => p.Course)
            .Empty().WithMessage(ValidationResource.VALIDATION_COURSE_MUST_BE_EMPTY)
                .When(p => !p.PersonType.Equals(PersonType.EGRESS));

        RuleFor(p => p.Course)
            .SetValidator(new CourseEntryModelValidator()!)
                .When(p => p.Course is not null);
    }
}
