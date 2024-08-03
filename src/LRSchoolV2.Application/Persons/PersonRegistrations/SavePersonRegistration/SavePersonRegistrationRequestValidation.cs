using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.SavePersonRegistration;

public class SavePersonRegistrationRequestValidation(IPersonRegistrationsRepository inPersonRegistrationsRepository) : AbstractValidator<SavePersonRegistrationRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SavePersonRegistrationRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateUniqueness();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateUniqueness() =>
        RuleFor(inRequest => inRequest.PersonRegistration)
            .MustAsync((inPersonRegistration, _) => inPersonRegistrationsRepository.IsPersonRegistrationUniqueAsync(inPersonRegistration))
            .WithMessage(SavePersonRegistrationRequestValidationErrors.PersonRegistrationNotUnique);
}