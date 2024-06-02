using Egress.Application.Commands.Highlights.RequestForHighlights;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class RequestForHighlightsCommandValidator : AbstractValidator<RequestForHighlightsCommand>
{
    #region Constants
    private const string URL_REGEX_EXPRESSION = @"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$";
    private const string LINK_PROPERTY_NAME = "link";
    private const string ADVERTISING_IMAGE_PROPERTY_NAME = "advertising_imagem";
    private const string VERACITY_FILES_PROPERTY_NAME = "veracity_files";
    private const int VERACITY_FILES_LIMIT = 5;
    private const long LIMIT_FILE_IN_BYTES = 2000000;
    private const long MEGABYTES_IN_BYTES = 1000000;
    private const string IMAGE_JPEG_MIME_TYPE = "image/jpeg";
    private const string IMAGE_PNG_MIME_TYPE = "image/png";
    private const string PERSON_ID_HEADER_MESSAGE = "Person-Id header";
    #endregion

    public RequestForHighlightsCommandValidator()
    {
        RuleFor(r => r.PersonId)
            .Must(id => !id.Equals(default)).WithMessage(string.Format(ValidationResource.VALIDATION_IS_REQUIRED, PERSON_ID_HEADER_MESSAGE));

        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(ValidationResource.VALIDATION_NOT_EMPTY);

        RuleFor(r => r.Link)
            .Matches(URL_REGEX_EXPRESSION)
                .When(r => !string.IsNullOrWhiteSpace(r.Link))
                    .WithMessage(string.Format(ValidationResource.VALIDATION_INVALID_FORMAT, LINK_PROPERTY_NAME, string.Empty));

        RuleFor(r => r.AdvertisingImage)
            .Must(ai => ai!.ContentType.Equals(IMAGE_JPEG_MIME_TYPE) || ai.ContentType.Equals(IMAGE_PNG_MIME_TYPE))
                .When(r => r.AdvertisingImage is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_CONTAINS_UNSUPPORTED_FORMAT, ADVERTISING_IMAGE_PROPERTY_NAME, $". Try using files of type {IMAGE_JPEG_MIME_TYPE} or {IMAGE_PNG_MIME_TYPE}"))
            .Must(ai => ai!.Length <= LIMIT_FILE_IN_BYTES)
                .When(r => r.AdvertisingImage is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_IS_LIMITED_TO, ADVERTISING_IMAGE_PROPERTY_NAME, $"{LIMIT_FILE_IN_BYTES/MEGABYTES_IN_BYTES}mb"));

        RuleFor(r => r.VeracityFiles)
            .Must(vf => vf!.Count <= VERACITY_FILES_LIMIT)
                .When(r => r.VeracityFiles is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_IS_LIMITED_TO, VERACITY_FILES_PROPERTY_NAME, $"{VERACITY_FILES_LIMIT} files"))
            .Must(vf => vf!.Any(f => f.Length <= LIMIT_FILE_IN_BYTES))
                .When(r => r.VeracityFiles is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_IS_LIMITED_TO, VERACITY_FILES_PROPERTY_NAME, $"{LIMIT_FILE_IN_BYTES/MEGABYTES_IN_BYTES}mb"));
    }
}