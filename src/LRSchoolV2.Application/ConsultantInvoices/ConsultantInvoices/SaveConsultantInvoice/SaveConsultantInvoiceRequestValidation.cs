using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Consultants.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SaveConsultantInvoice;

public partial class SaveConsultantInvoiceRequestValidation : AbstractValidator<SaveConsultantInvoiceRequest>
{
    private readonly IConsultantsRepository _consultantsRepository;

    public SaveConsultantInvoiceRequestValidation(IConsultantsRepository inConsultantsRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        _consultantsRepository = inConsultantsRepository;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveConsultantInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateNumber();
        ValidateConsultant();
        ValidateConsultantAddress();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateNumber() =>
        RuleFor(inRequest => inRequest.ConsultantInvoice.Number)
            .Must(inNumber => ConsultantInvoiceNumberRegex().Match(inNumber).Success)
            .WithMessage(inRequest => SaveConsultantInvoiceRequestValidationErrors.NumberInvalidFormat.Replace("{number}", inRequest.ConsultantInvoice.Number));
    
    [GeneratedRegex(@"^LRS\d{4}-\d{3}$")]
    private static partial Regex ConsultantInvoiceNumberRegex();
    
    private void ValidateConsultant() =>
        RuleFor(inRequest => inRequest.ConsultantInvoice.Consultant)
            .MustAsync((inConsultant, _) => _consultantsRepository.AnyConsultantAsync(inConsultant.Id))
            .WithMessage(inRequest => SaveConsultantInvoiceRequestValidationErrors.ConsultantNotFound.Replace("{personName}", inRequest.ConsultantInvoice.Consultant.GetFullName()));
    
    private void ValidateConsultantAddress() =>
        RuleFor(inRequest => inRequest.ConsultantInvoice.Consultant.Address)
            .Must(inAddress => !string.IsNullOrWhiteSpace(inAddress.Street) && !string.IsNullOrWhiteSpace(inAddress.ZipCode) && !string.IsNullOrWhiteSpace(inAddress.City))
            .WithMessage(inRequest => SaveConsultantInvoiceRequestValidationErrors.ConsultantHasNoAddress.Replace("{personName}", inRequest.ConsultantInvoice.Consultant.GetFullName()));
}