using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Consultants.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SaveConsultantQuote;

public partial class SaveConsultantQuoteRequestValidation : AbstractValidator<SaveConsultantQuoteRequest>
{
    private readonly IConsultantsRepository _consultantsRepository;

    public SaveConsultantQuoteRequestValidation(IConsultantsRepository inConsultantsRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        _consultantsRepository = inConsultantsRepository;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveConsultantQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateNumber();
        ValidateConsultant();
        ValidateConsultantAddress();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateNumber() =>
        RuleFor(inRequest => inRequest.ConsultantQuote.Number)
            .Must(inNumber => ConsultantQuoteNumberRegex().Match(inNumber).Success)
            .WithMessage(inRequest => SaveConsultantQuoteRequestValidationErrors.NumberInvalidFormat.Replace("{number}", inRequest.ConsultantQuote.Number));
    
    [GeneratedRegex(@"^\d{4}-\d{3}$")]
    private static partial Regex ConsultantQuoteNumberRegex();
    
    private void ValidateConsultant() =>
        RuleFor(inRequest => inRequest.ConsultantQuote.Consultant)
            .MustAsync((inConsultant, _) => _consultantsRepository.AnyConsultantAsync(inConsultant.Id))
            .WithMessage(inRequest => SaveConsultantQuoteRequestValidationErrors.ConsultantNotFound.Replace("{personName}", inRequest.ConsultantQuote.Consultant.GetFullName()));
    
    private void ValidateConsultantAddress() =>
        RuleFor(inRequest => inRequest.ConsultantQuote.Consultant.Address)
            .Must(inAddress => !string.IsNullOrWhiteSpace(inAddress.Street) && !string.IsNullOrWhiteSpace(inAddress.ZipCode) && !string.IsNullOrWhiteSpace(inAddress.City))
            .WithMessage(inRequest => SaveConsultantQuoteRequestValidationErrors.ConsultantHasNoAddress.Replace("{personName}", inRequest.ConsultantQuote.Consultant.GetFullName()));
}