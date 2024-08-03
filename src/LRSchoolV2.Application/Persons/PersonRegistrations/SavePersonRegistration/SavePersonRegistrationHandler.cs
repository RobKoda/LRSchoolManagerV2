using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.SavePersonRegistration;

public class SavePersonRegistrationHandler : IRequestHandler<SavePersonRegistrationCommand>
{
    private readonly IPersonRegistrationsRepository _personRegistrationsRepository;

    public SavePersonRegistrationHandler(IPersonRegistrationsRepository inPersonRegistrationsRepository)
    {
        _personRegistrationsRepository = inPersonRegistrationsRepository;
    }

    public Task Handle(SavePersonRegistrationCommand inRequest, CancellationToken inCancellationToken) => 
        _personRegistrationsRepository.SavePersonRegistrationAsync(inRequest.PersonRegistration);
}