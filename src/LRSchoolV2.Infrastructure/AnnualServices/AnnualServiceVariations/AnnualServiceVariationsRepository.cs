using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using Microsoft.EntityFrameworkCore;

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;

public class AnnualServiceVariationsRepository(IDbContextFactory<ApplicationContext> inContext) : IAnnualServiceVariationsRepository
{
    public Task<IEnumerable<AnnualServiceVariation>> GetAnnualServiceVariationsPerAnnualServiceAsync(Guid inAnnualServiceId) =>
        inContext.GetAllAsync<AnnualServiceVariationDataModel, AnnualServiceVariation>(inQueryable =>
            GetServiceVariationDataModelQueryable(inQueryable)
                .Where(inAnnualServiceVariation => inAnnualServiceVariation.AnnualServiceId == inAnnualServiceId)
        );

    public Task<bool> AnyAnnualServiceVariationAsync(Guid inAnnualServiceVariationId) =>
        inContext.AnyAsync<AnnualServiceVariationDataModel>(inAnnualServiceVariationId);

    public Task DeleteAnnualServiceVariationAsync(Guid inAnnualServiceVariationId) =>
        inContext.DeleteAsync<AnnualServiceVariationDataModel>(inAnnualServiceVariationId);

    public Task SaveAnnualServiceVariationAsync(AnnualServiceVariation inAnnualServiceVariation) =>
        inContext.SaveAsync<AnnualServiceVariationDataModel, AnnualServiceVariation>(inAnnualServiceVariation);

    public async Task<bool> IsAnnualServiceVariationUniqueAsync(AnnualServiceVariation inReferenceAnnualServiceVariation) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<AnnualServiceVariationDataModel>())
            .Where(inAnnualServiceVariation => inAnnualServiceVariation.Id != inReferenceAnnualServiceVariation.Id)
            .AllAsync(inAnnualServiceVariation =>
                inAnnualServiceVariation.AnnualServiceId != inReferenceAnnualServiceVariation.AnnualService.Id ||
                !string.Equals(inAnnualServiceVariation.Name.ToUpper(), inReferenceAnnualServiceVariation.Name.ToUpper())
            );

    public Task<bool> CanAnnualServiceVariationBeDeletedAsync(Guid inAnnualServiceVariationId) =>
        inContext.CanBeDeleted<AnnualServiceVariationDataModel>(inAnnualServiceVariationId);

    public Task<IEnumerable<AnnualServiceVariation>> GetAnnualServiceVariationsPerSchoolYearAsync(Guid inSchoolYearId) =>
        inContext.GetAllAsync<AnnualServiceVariationDataModel, AnnualServiceVariation>(inQueryable =>
            GetServiceVariationDataModelQueryable(inQueryable)
                .Where(inAnnualServiceVariation => inAnnualServiceVariation.YearlyPrices.Any(inYearlyPrice => inYearlyPrice.SchoolYearId == inSchoolYearId))
        );

    private static IQueryable<AnnualServiceVariationDataModel> GetServiceVariationDataModelQueryable(IQueryable<AnnualServiceVariationDataModel> inQueryable) =>
        inQueryable
            .Include(inAnnualServiceVariation => inAnnualServiceVariation.AnnualService)
            .Include(inAnnualServiceVariation => inAnnualServiceVariation.YearlyPrices);
}