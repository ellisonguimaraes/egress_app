using Egress.Application.Queries;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class GenericGetRandomQueryValidator<TResponse> : AbstractValidator<GenericGetRandomQuery<TResponse>>
{
    # region Constants
    private const int ACCEPTED_VALUE_QUANTITY_PROPERTY = 1;
    # endregion

    public GenericGetRandomQueryValidator()
    {
        RuleFor(t => t.Quantity)
            .NotNull().WithMessage(ValidationResource.VALIDATION_NOT_NULL)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .GreaterThanOrEqualTo(ACCEPTED_VALUE_QUANTITY_PROPERTY).WithMessage(ValidationResource.VALIDATION_GREATER_THEN_OR_EQUAL);
    }
}