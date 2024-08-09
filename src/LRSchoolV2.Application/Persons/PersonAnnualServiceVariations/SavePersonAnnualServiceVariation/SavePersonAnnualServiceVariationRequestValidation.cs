using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.SavePersonAnnualServiceVariation;

public class SavePersonAnnualServiceVariationRequestValidation(
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository
    ) : AbstractValidator<SavePersonAnnualServiceVariationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SavePersonAnnualServiceVariationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidatePaymentCount();
        ValidateRegistration();
        ValidateServiceHasPrice();
        ValidateUniqueness();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidatePaymentCount() =>
        RuleFor(inRequest => inRequest.PersonAnnualServiceVariation.PaymentsCount)
            .Must(inPaymentCount => inPaymentCount > 0)
            .WithMessage(SavePersonAnnualServiceVariationRequestValidationErrors.PaymentCountNotPositive);
    
    private void ValidateRegistration() => RuleFor(inRequest => inRequest.PersonAnnualServiceVariation)
        .MustAsync((inPersonServiceVariation, _) => inPersonRegistrationsRepository.IsPersonRegisteredForYearAsync(inPersonServiceVariation.Person.Id, inPersonServiceVariation.SchoolYear.Id))
        .WithMessage(SavePersonAnnualServiceVariationRequestValidationErrors.PersonNotRegisteredForYear);
    
    private void ValidateServiceHasPrice() => RuleFor(inRequest => inRequest.PersonAnnualServiceVariation)
        .MustAsync((inPersonServiceVariation, _) => inAnnualServiceVariationYearlyPricesRepository.AnyAnnualServiceVariationPriceForYearAsync(inPersonServiceVariation.AnnualServiceVariation.Id, inPersonServiceVariation.SchoolYear.Id))
        .WithMessage(SavePersonAnnualServiceVariationRequestValidationErrors.ServiceHasNoPrice);
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.PersonAnnualServiceVariation)
            .MustAsync((inPersonServiceVariation, _) => inPersonAnnualServiceVariationsRepository.IsPersonAnnualServiceVariationUniqueAsync(inPersonServiceVariation))
            .WithMessage(SavePersonAnnualServiceVariationRequestValidationErrors.PersonAnnualServiceVariationNotUnique);
}