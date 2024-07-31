// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Common;

public record SchoolYear(
    Guid Id, 
    DateTime StartDate, 
    DateTime EndDate,
    decimal MembershipFee)
{
    public string GetPeriodDisplay() => $"{StartDate.Year}/{EndDate.Year}";
    public bool IsCurrentPeriod() => DateTime.Today > StartDate && DateTime.Today < EndDate;
}
