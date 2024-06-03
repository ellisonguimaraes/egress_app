using Egress.Application.Commands.Testimony.RequestForTestimony;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class RequestForTestimonyCommandValidator : AbstractValidator<RequestForTestimonyCommand>
{
    #region Constants
    private const string PERSON_ID_HEADER_MESSAGE = "Person-Id header";
    #endregion

    public RequestForTestimonyCommandValidator()
    {
        RuleFor(r => r.PersonId)
            .Must(id => !id.Equals(default)).WithMessage(string.Format(ValidationResource.VALIDATION_IS_REQUIRED, PERSON_ID_HEADER_MESSAGE));

        RuleFor(r => r.Content)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    }
}