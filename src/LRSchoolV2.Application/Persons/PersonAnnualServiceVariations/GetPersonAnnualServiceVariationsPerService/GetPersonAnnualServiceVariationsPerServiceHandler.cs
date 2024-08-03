using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerService;

public class GetPersonAnnualServiceVariationsPerServiceHandler : IRequestHandler<GetPersonAnnualServiceVariationsPerServiceQuery, GetPersonAnnualServiceVariationsPerServiceResponse>
{
    private readonly IPersonAnnualServiceVariationsRepository _repository;

    public GetPersonAnnualServiceVariationsPerServiceHandler(IPersonAnnualServiceVariationsRepository inRepository)
    {
        _repository = inRepository;
    }

    public async Task<GetPersonAnnualServiceVariationsPerServiceResponse> Handle(GetPersonAnnualServiceVariationsPerServiceQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _repository.GetPersonAnnualServiceVariationsPerAnnualServiceAsync(inRequest.AnnualServiceId));
}