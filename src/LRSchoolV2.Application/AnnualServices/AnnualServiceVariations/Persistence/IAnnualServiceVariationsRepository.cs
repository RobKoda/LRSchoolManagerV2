using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;

public interface IAnnualServiceVariationsRepository : IRepository
{
    Task<IEnumerable<AnnualServiceVariation>> GetAnnualServiceVariationsPerAnnualServiceAsync(Guid inAnnualServiceId);
    Task<bool> AnyAnnualServiceVariationAsync(Guid inAnnualServiceVariationId);
    Task DeleteAnnualServiceVariationAsync(Guid inAnnualServiceVariationId);
    Task SaveAnnualServiceVariationAsync(AnnualServiceVariation inAnnualServiceVariation);
    Task<bool> IsAnnualServiceVariationUniqueAsync(AnnualServiceVariation inReferenceAnnualServiceVariation);
    Task<bool> CanAnnualServiceVariationBeDeletedAsync(Guid inAnnualServiceVariationId);
    Task<IEnumerable<AnnualServiceVariation>> GetAnnualServiceVariationsPerSchoolYearAsync(Guid inSchoolYearId);
}