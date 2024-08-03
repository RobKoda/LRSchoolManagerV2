using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.SavePerson;

public record SavePersonCommand(Person Person) : IRequest;
