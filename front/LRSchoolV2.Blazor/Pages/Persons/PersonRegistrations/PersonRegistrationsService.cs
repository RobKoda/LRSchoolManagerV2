using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.Persons.PersonRegistrations.DeletePersonRegistration;
using LRSchoolV2.Application.Persons.PersonRegistrations.GetPersonRegistrationsPerPerson;
using LRSchoolV2.Application.Persons.PersonRegistrations.SavePersonRegistration;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.Persons;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.Persons.PersonRegistrations;

public class PersonRegistrationsService(
    ISender inMediator,
    IValidator<DeletePersonRegistrationRequest> inDeletePersonRegistrationRequestValidator,
    IValidator<SavePersonRegistrationRequest> inSavePersonRegistrationRequestValidator,
    IValidator<GetPersonRegistrationsPerPersonRequest> inGetPersonRegistrationsPerPersonRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<PersonRegistration>>> GetPersonRegistrationsPerPersonAsync(Guid inPersonId)
    {
        var request = new GetPersonRegistrationsPerPersonRequest(inPersonId);
        var result = await inMediator.SendRequestWithValidation<GetPersonRegistrationsPerPersonRequest, GetPersonRegistrationsPerPersonQuery, GetPersonRegistrationsPerPersonResponse>(request, inGetPersonRegistrationsPerPersonRequestValidator);
        return result.Map<IEnumerable<PersonRegistration>>(inSuccess => inSuccess.PersonRegistrations);
    }

    public Task<Validation<string, Unit>> DeletePersonRegistrationAsync(PersonRegistration inPersonRegistration)
    {
        var request = new DeletePersonRegistrationRequest(inPersonRegistration);
        return inMediator.SendRequestWithValidation<DeletePersonRegistrationRequest, DeletePersonRegistrationCommand>(request, inDeletePersonRegistrationRequestValidator);
    }

    public Task<Validation<string, Unit>> SavePersonRegistrationAsync(PersonRegistration inPersonRegistration)
    {
        var request = new SavePersonRegistrationRequest(inPersonRegistration);
        return inMediator.SendRequestWithValidation<SavePersonRegistrationRequest, SavePersonRegistrationCommand>(request, inSavePersonRegistrationRequestValidator);
    }
}