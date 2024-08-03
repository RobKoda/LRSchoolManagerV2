using FluentValidation;
using FluentValidation.Results;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.Documents.SaveDocument;

public class SaveDocumentRequestValidation : AbstractValidator<SaveDocumentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveDocumentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateContentType();
        ValidateFileName();

        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateContentType() =>
        RuleFor(inRequest => inRequest.Document.ContentType)
            .Must(inContentType => inContentType.Length <= 128)
            .WithMessage(SaveDocumentRequestValidationErrors.ContentTypeTooLong);

    private void ValidateFileName() =>
        RuleFor(inRequest => inRequest.Document.FileName)
            .Must(inFileName => inFileName.Length <= 1024)
            .WithMessage(SaveDocumentRequestValidationErrors.FileNameTooLong);
}