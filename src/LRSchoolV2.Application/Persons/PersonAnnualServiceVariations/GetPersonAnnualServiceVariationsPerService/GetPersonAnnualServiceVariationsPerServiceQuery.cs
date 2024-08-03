using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerService;

public record GetPersonAnnualServiceVariationsPerServiceQuery(Guid AnnualServiceId) : IRequest<GetPersonAnnualServiceVariationsPerServiceResponse>;