using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.Persons.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerPerson;

public class GetPersonAnnualServiceVariationsPerPersonValidation(IPersonsRepository inPersonsRepository) : AbstractValidator<GetPersonAnnualServiceVariationsPerPersonRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetPersonAnnualServiceVariationsPerPersonRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidatePersonId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidatePersonId() =>
        RuleFor(inRequest => inRequest.PersonId)
            .MustAsync((inPersonId, _) => inPersonsRepository.AnyPersonAsync(inPersonId))
            .WithMessage(GetPersonAnnualServiceVariationsPerPersonValidationErrors.PersonNotFound);
}