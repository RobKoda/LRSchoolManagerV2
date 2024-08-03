using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerPerson;

public class GetPersonAnnualServiceVariationsPerPersonHandler : IRequestHandler<GetPersonAnnualServiceVariationsPerPersonQuery, GetPersonAnnualServiceVariationsPerPersonResponse>
{
    private readonly IPersonAnnualServiceVariationsRepository _repository;

    public GetPersonAnnualServiceVariationsPerPersonHandler(IPersonAnnualServiceVariationsRepository inRepository)
    {
        _repository = inRepository;
    }

    public async Task<GetPersonAnnualServiceVariationsPerPersonResponse> Handle(GetPersonAnnualServiceVariationsPerPersonQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _repository.GetPersonAnnualServiceVariationsPerPersonAsync(inRequest.PersonId));
}