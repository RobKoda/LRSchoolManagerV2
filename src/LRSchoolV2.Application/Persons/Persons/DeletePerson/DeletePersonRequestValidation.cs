using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.Persons.Persons.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.DeletePerson;

public class DeletePersonRequestValidation(IPersonsRepository inPersonsRepository) : AbstractValidator<DeletePersonRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeletePersonRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.Person.Id)
            .MustAsync((inPersonId, _) => inPersonsRepository.AnyPersonAsync(inPersonId))
            .WithMessage(DeletePersonRequestValidationErrors.PersonNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.Person.Id)
            .MustAsync((inPersonId, _) => inPersonsRepository.CanPersonBeDeletedAsync(inPersonId))
            .WithMessage(DeletePersonRequestValidationErrors.PersonCannotBeDeleted);
}