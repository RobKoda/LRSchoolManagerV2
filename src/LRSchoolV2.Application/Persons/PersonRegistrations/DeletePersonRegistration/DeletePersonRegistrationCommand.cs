using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonRegistrations.DeletePersonRegistration;

public record DeletePersonRegistrationCommand(PersonRegistration PersonRegistration) : IRequest;
