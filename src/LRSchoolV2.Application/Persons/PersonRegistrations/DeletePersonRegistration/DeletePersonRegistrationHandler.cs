using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.DeletePersonRegistration;

public class DeletePersonRegistrationHandler : IRequestHandler<DeletePersonRegistrationCommand>
{
    private readonly IPersonRegistrationsRepository _personRegistrationsRepository;

    public DeletePersonRegistrationHandler(IPersonRegistrationsRepository inPersonRegistrationsRepository)
    {
        _personRegistrationsRepository = inPersonRegistrationsRepository;
    }

    public Task Handle(DeletePersonRegistrationCommand inRequest, CancellationToken inCancellationToken) => 
        _personRegistrationsRepository.DeletePersonRegistrationAsync(inRequest.PersonRegistration.Id);
}