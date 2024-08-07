using LRSchoolV2.Application.Persons.Persons.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.GetUnbalancedPersons;

public class GetUnbalancedPersonsHandler(IPersonsRepository inRepository) : IRequestHandler<GetUnbalancedPersonsQuery, GetUnbalancedPersonsResponse>
{
    public async Task<GetUnbalancedPersonsResponse> Handle(GetUnbalancedPersonsQuery inRequest, CancellationToken inCancellationToken) => 
        new((await inRepository.GetPersonsAsync()).Where(inPerson => inPerson.GetBalance() != 0));
}