using Egress.Application.Commands.Person.CreateBasicPerson;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class CourseEntryModelValidator : AbstractValidator<CourseEntryModel>
{
    #region Constants
    private const string PROPERTY_NAME = "{PropertyName}";
    private const string MAT_ERROR_MESSAGE_COMPLETING = ". Contains only digits.";
    private const string SEMESTER_ERROR_MESSAGE_COMPLETING = ". Valid format YYYY.S, where S can assume number 1 or number 2.";
    private const string MAT_REGEX = @"^\d*$";
    private const string SEMESTER_REGEX = @"^\d{4}\.[12]$";
    #endregion

    public CourseEntryModelValidator()
    {
        RuleFor(c => c.CourseId)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(c => c.BeginningSemester)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .Matches(SEMESTER_REGEX).WithMessage(string.Format(ValidationResource.VALIDATION_INVALID_FORMAT, PROPERTY_NAME, SEMESTER_ERROR_MESSAGE_COMPLETING));

        RuleFor(c => c.FinalSemester)
            .Matches(SEMESTER_REGEX).WithMessage(string.Format(ValidationResource.VALIDATION_INVALID_FORMAT, PROPERTY_NAME, SEMESTER_ERROR_MESSAGE_COMPLETING))
                .When(c => !string.IsNullOrEmpty(c.FinalSemester));

        RuleFor(c => c.Mat)
            .Matches(MAT_REGEX).WithMessage(string.Format(ValidationResource.VALIDATION_INVALID_FORMAT, PROPERTY_NAME, MAT_ERROR_MESSAGE_COMPLETING))
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(p => p.Level)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .IsInEnum().WithMessage(ValidationResource.VALIDATION_IS_INVALID);

        RuleFor(p => p.Modality)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY)
            .IsInEnum().WithMessage(ValidationResource.VALIDATION_IS_INVALID);
    }
}
