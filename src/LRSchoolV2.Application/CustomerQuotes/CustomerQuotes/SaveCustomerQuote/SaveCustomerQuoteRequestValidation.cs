using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.Persons.Persistence;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SaveCustomerQuote;

public partial class SaveCustomerQuoteRequestValidation : AbstractValidator<SaveCustomerQuoteRequest>
{
    private readonly IPersonsRepository _personsRepository;

    public SaveCustomerQuoteRequestValidation(IPersonsRepository inPersonsRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        _personsRepository = inPersonsRepository;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCustomerQuoteRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateNumber();
        ValidateCustomer();
        ValidateCustomerAddress();
        
        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateNumber() =>
        RuleFor(inRequest => inRequest.CustomerQuote.Number)
            .Must(inNumber => CustomerQuoteNumberRegex().Match(inNumber).Success)
            .WithMessage(inRequest => SaveCustomerQuoteRequestValidationErrors.NumberInvalidFormat.Replace("{number}", inRequest.CustomerQuote.Number));
    
    [GeneratedRegex(@"^\d{4}-\d{3}$")]
    private static partial Regex CustomerQuoteNumberRegex();
    
    private void ValidateCustomer() =>
        RuleFor(inRequest => inRequest.CustomerQuote.Customer)
            .MustAsync((inPerson, _) => _personsRepository.AnyPersonAsync(inPerson.Id))
            .WithMessage(inRequest => SaveCustomerQuoteRequestValidationErrors.CustomerNotFound.Replace("{personName}", inRequest.CustomerQuote.Customer.GetFullName()));
    
    private void ValidateCustomerAddress() =>
        RuleFor(inRequest => inRequest.CustomerQuote.Customer.Address)
            .Must(inAddress => !string.IsNullOrWhiteSpace(inAddress.Street) && !string.IsNullOrWhiteSpace(inAddress.ZipCode) && !string.IsNullOrWhiteSpace(inAddress.City))
            .WithMessage(inRequest => SaveCustomerQuoteRequestValidationErrors.CustomerHasNoAddress.Replace("{personName}", inRequest.CustomerQuote.Customer.GetFullName()));
}