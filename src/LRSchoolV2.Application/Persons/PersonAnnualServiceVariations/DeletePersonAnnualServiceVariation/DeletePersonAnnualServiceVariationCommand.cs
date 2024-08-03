using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.DeletePersonAnnualServiceVariation;

public record DeletePersonAnnualServiceVariationCommand(PersonAnnualServiceVariation PersonAnnualServiceVariation) : IRequest;
