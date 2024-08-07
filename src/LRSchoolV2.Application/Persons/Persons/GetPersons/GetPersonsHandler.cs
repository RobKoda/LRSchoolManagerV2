using LRSchoolV2.Application.Persons.Persons.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.GetPersons;

public class GetPersonsHandler(IPersonsRepository inRepository) : IRequestHandler<GetPersonsQuery, GetPersonsResponse>
{
    public async Task<GetPersonsResponse> Handle(GetPersonsQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inRepository.GetPersonsAsync());
}