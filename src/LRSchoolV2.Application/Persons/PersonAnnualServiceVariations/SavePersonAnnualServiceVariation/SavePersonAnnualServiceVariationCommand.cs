using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.SavePersonAnnualServiceVariation;

public record SavePersonAnnualServiceVariationCommand(PersonAnnualServiceVariation PersonAnnualServiceVariation) : IRequest;
