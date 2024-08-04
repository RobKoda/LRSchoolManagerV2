using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices;

public class AnnualServiceVariationYearlyPricesRepository(IDbContextFactory<ApplicationContext> inContext) : IAnnualServiceVariationYearlyPricesRepository
{
    public Task<IEnumerable<AnnualServiceVariationYearlyPrice>> GetAnnualServiceVariationYearlyPricesAsync() =>
        inContext.GetAllAsync<AnnualServiceVariationYearlyPriceDataModel, AnnualServiceVariationYearlyPrice>();

    public Task<IEnumerable<AnnualServiceVariationYearlyPrice>> GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationAsync(Guid inAnnualServiceVariationId) =>
        inContext.GetAllAsync<AnnualServiceVariationYearlyPriceDataModel, AnnualServiceVariationYearlyPrice>(inQueryable => inQueryable
            .Where(inYearlyPrice => inYearlyPrice.AnnualServiceVariationId == inAnnualServiceVariationId)
        );

    public Task<bool> AnyAnnualServiceVariationYearlyPriceAsync(Guid inAnnualServiceVariationYearlyPriceId) =>
        inContext.AnyAsync<AnnualServiceVariationYearlyPriceDataModel>(inAnnualServiceVariationYearlyPriceId);

    public Task DeleteAnnualServiceVariationYearlyPriceAsync(Guid inAnnualServiceVariationYearlyPriceId) =>
        inContext.DeleteAsync<AnnualServiceVariationYearlyPriceDataModel>(inAnnualServiceVariationYearlyPriceId);

    public Task SaveAnnualServiceVariationYearlyPriceAsync(AnnualServiceVariationYearlyPrice inAnnualServiceVariationYearlyPrice) =>
        inContext.SaveAsync<AnnualServiceVariationYearlyPriceDataModel, AnnualServiceVariationYearlyPrice>(inAnnualServiceVariationYearlyPrice);

    public async Task<bool> IsAnnualServiceVariationYearlyPriceUniqueAsync(AnnualServiceVariationYearlyPrice inReferenceAnnualServiceVariationYearlyPrice) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<AnnualServiceVariationYearlyPriceDataModel>())
            .Where(inAnnualServiceVariationYearlyPrice => inAnnualServiceVariationYearlyPrice.Id != inReferenceAnnualServiceVariationYearlyPrice.Id)
            .AllAsync(inAnnualServiceVariationYearlyPrice =>
                inAnnualServiceVariationYearlyPrice.SchoolYearId != inReferenceAnnualServiceVariationYearlyPrice.SchoolYear.Id ||
                inAnnualServiceVariationYearlyPrice.AnnualServiceVariationId != inReferenceAnnualServiceVariationYearlyPrice.AnnualServiceVariationId);

    public Task<bool> CanAnnualServiceVariationYearlyPriceBeDeletedAsync(Guid inAnnualServiceVariationYearlyPriceId) =>
        inContext.CanBeDeleted<AnnualServiceVariationYearlyPriceDataModel>(inAnnualServiceVariationYearlyPriceId);

    public Task<IEnumerable<AnnualServiceVariationYearlyPrice>> GetCurrentYearlyPricesAsync(Guid inSchoolYearId) =>
        inContext.GetAllAsync<AnnualServiceVariationYearlyPriceDataModel, AnnualServiceVariationYearlyPrice>(inQueryable => inQueryable
            .Where(inYearlyPrice => inYearlyPrice.SchoolYearId == inSchoolYearId)
        );
}