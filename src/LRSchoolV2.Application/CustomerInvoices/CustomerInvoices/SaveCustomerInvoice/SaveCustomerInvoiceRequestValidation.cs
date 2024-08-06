using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.Persons.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SaveCustomerInvoice;

public partial class SaveCustomerInvoiceRequestValidation : AbstractValidator<SaveCustomerInvoiceRequest>
{
    private readonly IPersonsRepository _personsRepository;

    public SaveCustomerInvoiceRequestValidation(IPersonsRepository inPersonsRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        _personsRepository = inPersonsRepository;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCustomerInvoiceRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateNumber();
        ValidateCustomer();
        ValidateCustomerAddress();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateNumber() =>
        RuleFor(inRequest => inRequest.CustomerInvoice.Number)
            .Must(inNumber => CustomerInvoiceNumberRegex().Match(inNumber).Success)
            .WithMessage(inRequest => SaveCustomerInvoiceRequestValidationErrors.NumberInvalidFormat.Replace("{number}", inRequest.CustomerInvoice.Number));
    
    [GeneratedRegex(@"^\d{4}-\d{3}$")]
    private static partial Regex CustomerInvoiceNumberRegex();
    
    private void ValidateCustomer() =>
        RuleFor(inRequest => inRequest.CustomerInvoice.Customer)
            .MustAsync((inPerson, _) => _personsRepository.AnyPersonAsync(inPerson.Id))
            .WithMessage(inRequest => SaveCustomerInvoiceRequestValidationErrors.CustomerNotFound.Replace("{personName}", inRequest.CustomerInvoice.Customer.GetFullName()));
    
    private void ValidateCustomerAddress() =>
        RuleFor(inRequest => inRequest.CustomerInvoice.Customer.Address)
            .Must(inAddress => !string.IsNullOrWhiteSpace(inAddress.Street) && !string.IsNullOrWhiteSpace(inAddress.ZipCode) && !string.IsNullOrWhiteSpace(inAddress.City))
            .WithMessage(inRequest => SaveCustomerInvoiceRequestValidationErrors.CustomerHasNoAddress.Replace("{personName}", inRequest.CustomerInvoice.Customer.GetFullName()));
}