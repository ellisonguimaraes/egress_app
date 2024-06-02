using Egress.Application.Commands.Person.CreateBasicPersonBatch;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.Application.Validators;

public class CreateBasicPersonBatchCommandValidator : AbstractValidator<CreateBasicPersonBatchCommand>
{
    #region Constants
    private const string CSV_MIME_TYPE = "text/csv";
    private const long LIMIT_FILE_IN_BYTES = 10000000;
    private const long MEGABYTES_IN_BYTES = 1000000;
    private const string BATCH_PROPERTY_NAME = "batch";
    #endregion

    public CreateBasicPersonBatchCommandValidator()
    {
        RuleFor(c => c.Batch)
            .NotNull()
                .WithMessage(ValidationResource.VALIDATION_NOT_NULL);
            
        RuleFor(c => c.Batch)
            .Must(b => b.ContentType.Equals(CSV_MIME_TYPE))
                .When(c => c.Batch is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_CONTAINS_UNSUPPORTED_FORMAT, BATCH_PROPERTY_NAME, $". Using CSV file"))
            .Must(b => b.Length <= LIMIT_FILE_IN_BYTES)
                .When(c => c.Batch is not null)
                    .WithMessage(string.Format(ValidationResource.VALIDATION_IS_LIMITED_TO, BATCH_PROPERTY_NAME, $"{LIMIT_FILE_IN_BYTES/MEGABYTES_IN_BYTES}mb"));
    }
}
