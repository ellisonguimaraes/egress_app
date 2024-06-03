using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application;

public class UpdateProfileImageCommandValidator : AbstractValidator<UpdateProfileImageCommand>
{
    #region Constants
    private const long LIMIT_FILE_IN_BYTES = 2000000;
    private const long MEGABYTES_IN_BYTES = 1000000;
    private const string IMAGE_JPEG_MIME_TYPE = "image/jpeg";
    private const string IMAGE_PNG_MIME_TYPE = "image/png";
    private const string PROPERTY_NAME = "{PropertyName}";
    #endregion
    
    public UpdateProfileImageCommandValidator()
    {
        RuleFor(p => p.PerfilImage)
            .NotNull().WithMessage(ValidationResource.VALIDATION_NOT_NULL);

        RuleFor(p => p.PerfilImage)
            .Must(pi => pi!.ContentType.Equals(IMAGE_JPEG_MIME_TYPE) || pi.ContentType.Equals(IMAGE_PNG_MIME_TYPE))
                .When(p => p.PerfilImage is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_CONTAINS_UNSUPPORTED_FORMAT, PROPERTY_NAME, $". Try using files of type {IMAGE_JPEG_MIME_TYPE} or {IMAGE_PNG_MIME_TYPE}"))
            .Must(pi => pi!.Length <= LIMIT_FILE_IN_BYTES)
                .When(p => p.PerfilImage is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_IS_LIMITED_TO, PROPERTY_NAME, $"{LIMIT_FILE_IN_BYTES/MEGABYTES_IN_BYTES}mb"));

        RuleFor(p => p.PersonId)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);
    }
}
