using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.GetPersonRegistrationsPerPerson;

public record GetPersonRegistrationsPerPersonQuery(Guid PersonId) : IRequest<GetPersonRegistrationsPerPersonResponse>;