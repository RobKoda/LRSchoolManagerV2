// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Domain.AnnualServices;

public record AnnualServiceVariation(
    Guid Id,
    AnnualService AnnualService,
    string Name,
    string InvoiceName,
    AnnualServiceVariationYearlyPrice? CurrentYearlyPrice)
{
    public string GetFullName() => $"{AnnualService.Name} {Name}";
    public decimal GetPrice() => CurrentYearlyPrice?.Price ?? 0m;
    public decimal GetMargin() => CurrentYearlyPrice?.Margin ?? 0m;
}