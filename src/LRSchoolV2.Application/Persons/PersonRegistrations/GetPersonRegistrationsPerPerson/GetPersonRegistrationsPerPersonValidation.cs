using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.Persons.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.GetPersonRegistrationsPerPerson;

public class GetPersonRegistrationsPerPersonValidation(IPersonsRepository inPersonsRepository) : AbstractValidator<GetPersonRegistrationsPerPersonRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetPersonRegistrationsPerPersonRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidatePersonId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidatePersonId() =>
        RuleFor(inRequest => inRequest.PersonId)
            .MustAsync((inPersonId, _) => inPersonsRepository.AnyPersonAsync(inPersonId))
            .WithMessage(GetPersonRegistrationsPerPersonValidationErrors.PersonNotFound);
}