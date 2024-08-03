using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;

public interface IAnnualServiceVariationYearlyPricesRepository : IRepository
{
    Task<IEnumerable<AnnualServiceVariationYearlyPrice>> GetAnnualServiceVariationYearlyPricesAsync();
    Task<IEnumerable<AnnualServiceVariationYearlyPrice>> GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationAsync(Guid inAnnualServiceVariationId);
    Task<bool> AnyAnnualServiceVariationYearlyPriceAsync(Guid inAnnualServiceVariationYearlyPriceId);
    Task DeleteAnnualServiceVariationYearlyPriceAsync(Guid inAnnualServiceVariationYearlyPriceId);
    Task SaveAnnualServiceVariationYearlyPriceAsync(AnnualServiceVariationYearlyPrice inAnnualServiceVariationYearlyPrice);
    Task<bool> IsAnnualServiceVariationYearlyPriceUniqueAsync(AnnualServiceVariationYearlyPrice inReferenceAnnualServiceVariationYearlyPrice);
    Task<bool> CanAnnualServiceVariationYearlyPriceBeDeletedAsync(Guid inAnnualServiceVariationYearlyPriceId);
    Task<IEnumerable<AnnualServiceVariationYearlyPrice>> GetCurrentYearlyPricesAsync(Guid inSchoolYearId);
}