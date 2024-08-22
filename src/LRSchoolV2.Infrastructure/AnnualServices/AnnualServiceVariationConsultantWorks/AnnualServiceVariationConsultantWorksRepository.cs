using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks;

public class AnnualServiceVariationConsultantWorksRepository(IDbContextFactory<ApplicationContext> inContext) : IAnnualServiceVariationConsultantWorksRepository
{
    public Task<IEnumerable<AnnualServiceVariationConsultantWork>> GetAnnualServiceVariationConsultantWorksAsync() =>
        inContext.GetAllAsync<AnnualServiceVariationConsultantWorkDataModel, AnnualServiceVariationConsultantWork>(GetAnnualServiceVariationConsultantWorkDataModelQueryable);
    
    public Task<IEnumerable<AnnualServiceVariationConsultantWork>> GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationAsync(Guid inAnnualServiceVariationId) =>
        inContext.GetAllAsync<AnnualServiceVariationConsultantWorkDataModel, AnnualServiceVariationConsultantWork>(inQueryable => 
            GetAnnualServiceVariationConsultantWorkDataModelQueryable(inQueryable)
                .Where(inConsultantWork => inConsultantWork.AnnualServiceVariationId == inAnnualServiceVariationId)
        );

    public Task<bool> AnyAnnualServiceVariationConsultantWorkAsync(Guid inAnnualServiceVariationConsultantWorkId) =>
        inContext.AnyAsync<AnnualServiceVariationConsultantWorkDataModel>(inAnnualServiceVariationConsultantWorkId);

    public Task DeleteAnnualServiceVariationConsultantWorkAsync(Guid inAnnualServiceVariationConsultantWorkId) =>
        inContext.DeleteAsync<AnnualServiceVariationConsultantWorkDataModel>(inAnnualServiceVariationConsultantWorkId);

    public Task SaveAnnualServiceVariationConsultantWorkAsync(AnnualServiceVariationConsultantWork inAnnualServiceVariationConsultantWork) =>
        inContext.SaveAsync<AnnualServiceVariationConsultantWorkDataModel, AnnualServiceVariationConsultantWork>(inAnnualServiceVariationConsultantWork);

    public async Task<bool> IsAnnualServiceVariationConsultantWorkUniqueAsync(AnnualServiceVariationConsultantWork inReferenceAnnualServiceVariationConsultantWork) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<AnnualServiceVariationConsultantWorkDataModel>())
            .Where(inAnnualServiceVariationConsultantWork => inAnnualServiceVariationConsultantWork.Id != inReferenceAnnualServiceVariationConsultantWork.Id)
            .AllAsync(inAnnualServiceVariationConsultantWork =>
                inAnnualServiceVariationConsultantWork.AnnualServiceVariationId != inReferenceAnnualServiceVariationConsultantWork.AnnualServiceVariation.Id ||
                inAnnualServiceVariationConsultantWork.ConsultantId != inReferenceAnnualServiceVariationConsultantWork.Consultant.Id ||
                inAnnualServiceVariationConsultantWork.SchoolYearId != inReferenceAnnualServiceVariationConsultantWork.SchoolYear.Id);

    public Task<bool> CanAnnualServiceVariationConsultantWorkBeDeletedAsync(Guid inServiceVariationConsultantWorkId) =>
        inContext.CanBeDeleted<AnnualServiceVariationConsultantWorkDataModel>(inServiceVariationConsultantWorkId);
    
    private static IQueryable<AnnualServiceVariationConsultantWorkDataModel> GetAnnualServiceVariationConsultantWorkDataModelQueryable(IQueryable<AnnualServiceVariationConsultantWorkDataModel> inQueryable) =>
        inQueryable
            .Include(inWork => inWork.AnnualServiceVariation)
            .ThenInclude(inAnnualServiceVariation => inAnnualServiceVariation!.AnnualService);
}