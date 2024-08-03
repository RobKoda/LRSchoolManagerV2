using LRSchoolV2.Application.Persons.Persons.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.GetPersons;

public class GetPersonsHandler : IRequestHandler<GetPersonsQuery, GetPersonsResponse>
{
    private readonly IPersonsRepository _repository;
    
    public GetPersonsHandler(IPersonsRepository inRepository)
    {
        _repository = inRepository;
    }

    public async Task<GetPersonsResponse> Handle(GetPersonsQuery inRequest, CancellationToken inCancellationToken)
    {
        return new GetPersonsResponse(await _repository.GetPersonsAsync());
    }
}