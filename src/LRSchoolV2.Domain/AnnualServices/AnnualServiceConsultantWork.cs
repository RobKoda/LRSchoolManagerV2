using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualServiceConsultantWork(
    Guid Id,
    AnnualService AnnualService,
    SchoolYear SchoolYear,
    Consultant Consultant,
    decimal CommonWorkHours,
    string CommonWorkHoursComment
);