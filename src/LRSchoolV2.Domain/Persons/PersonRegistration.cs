using LRSchoolV2.Domain.Common;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Persons;

public record PersonRegistration(
    Guid Id,
    Person Person,
    SchoolYear SchoolYear,
    bool ImageRightsGranted,
    Person? BilledPerson);