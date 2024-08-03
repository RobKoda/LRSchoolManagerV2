using LRSchoolV2.Domain.Common;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualServiceVariationYearlyPrice(
    Guid Id,
    Guid AnnualServiceVariationId,
    SchoolYear SchoolYear,
    decimal Price,
    decimal Margin
);