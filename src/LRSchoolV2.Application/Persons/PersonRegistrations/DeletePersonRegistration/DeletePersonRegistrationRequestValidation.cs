using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.DeletePersonRegistration;

public class DeletePersonRegistrationRequestValidation(
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository
    ) : AbstractValidator<DeletePersonRegistrationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeletePersonRegistrationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        ValidateNoAnnualServiceVariation();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.PersonRegistration.Id)
            .MustAsync((inPersonRegistrationId, _) => inPersonRegistrationsRepository.AnyPersonRegistrationAsync(inPersonRegistrationId))
            .WithMessage(DeletePersonRegistrationRequestValidationErrors.PersonRegistrationNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.PersonRegistration.Id)
            .MustAsync((inPersonRegistrationId, _) => inPersonRegistrationsRepository.CanPersonRegistrationBeDeletedAsync(inPersonRegistrationId))
            .WithMessage(DeletePersonRegistrationRequestValidationErrors.PersonRegistrationCannotBeDeleted);
    
    private void ValidateNoAnnualServiceVariation() =>
        RuleFor(inRequest => inRequest.PersonRegistration)
            .MustAsync(async (inPersonRegistration, _) => !await inPersonAnnualServiceVariationsRepository.AnyPersonAnnualServiceVariationPerPersonAndSchoolYearAsync(inPersonRegistration.Person.Id, inPersonRegistration.SchoolYear.Id))
            .WithMessage(DeletePersonRegistrationRequestValidationErrors.PersonRegistrationHasAssociatedAnnualServiceVariations);
}