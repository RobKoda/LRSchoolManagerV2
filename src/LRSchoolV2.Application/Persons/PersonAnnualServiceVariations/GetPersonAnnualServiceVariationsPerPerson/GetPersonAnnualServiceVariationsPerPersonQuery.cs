using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerPerson;

public record GetPersonAnnualServiceVariationsPerPersonQuery(Guid PersonId) : IRequest<GetPersonAnnualServiceVariationsPerPersonResponse>;