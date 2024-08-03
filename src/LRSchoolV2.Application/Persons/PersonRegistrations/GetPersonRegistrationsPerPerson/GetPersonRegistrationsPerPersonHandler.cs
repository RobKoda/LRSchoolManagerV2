using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.GetPersonRegistrationsPerPerson;

public class GetPersonRegistrationsPerPersonHandler : IRequestHandler<GetPersonRegistrationsPerPersonQuery, GetPersonRegistrationsPerPersonResponse>
{
    private readonly IPersonRegistrationsRepository _repository;

    public GetPersonRegistrationsPerPersonHandler(IPersonRegistrationsRepository inRepository)
    {
        _repository = inRepository;
    }

    public async Task<GetPersonRegistrationsPerPersonResponse> Handle(GetPersonRegistrationsPerPersonQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _repository.GetPersonRegistrationsPerPersonAsync(inRequest.PersonId));
}