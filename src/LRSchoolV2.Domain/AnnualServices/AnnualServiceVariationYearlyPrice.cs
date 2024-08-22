using LRSchoolV2.Domain.Common;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualServiceVariationYearlyPrice(
    Guid Id,
    AnnualServiceVariation AnnualServiceVariation,
    SchoolYear SchoolYear,
    decimal Price,
    decimal Margin
);