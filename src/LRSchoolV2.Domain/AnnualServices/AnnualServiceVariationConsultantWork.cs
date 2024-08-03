using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualServiceVariationConsultantWork(
    Guid Id,
    Guid AnnualServiceVariationId,
    SchoolYear SchoolYear,
    Consultant Consultant,
    decimal IndividualWorkHours,
    string IndividualWorkHoursComment
);
