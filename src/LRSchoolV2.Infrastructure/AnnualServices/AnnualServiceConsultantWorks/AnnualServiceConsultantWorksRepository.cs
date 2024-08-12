using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;

public class AnnualServiceConsultantWorksRepository(IDbContextFactory<ApplicationContext> inContext) : IAnnualServiceConsultantWorksRepository
{
    public Task<IEnumerable<AnnualServiceConsultantWork>> GetAnnualServiceConsultantWorksPerAnnualServiceAsync(Guid inAnnualServiceId) =>
        inContext.GetAllAsync<AnnualServiceConsultantWorkDataModel, AnnualServiceConsultantWork>(inQueryable => inQueryable
            .Where(inConsultantWork => inConsultantWork.AnnualServiceId == inAnnualServiceId)
        );
    
    public Task<IEnumerable<AnnualServiceConsultantWork>> GetAnnualServiceConsultantWorksPerSchoolYearAsync(Guid inSchoolYearId) =>
        inContext.GetAllAsync<AnnualServiceConsultantWorkDataModel, AnnualServiceConsultantWork>(inQueryable => inQueryable
            .Where(inConsultantWork => inConsultantWork.SchoolYearId == inSchoolYearId)
        );
    
    public Task<bool> AnyAnnualServiceConsultantWorkAsync(Guid inAnnualServiceConsultantWorkId) =>
        inContext.AnyAsync<AnnualServiceConsultantWorkDataModel>(inAnnualServiceConsultantWorkId);

    public Task DeleteAnnualServiceConsultantWorkAsync(Guid inAnnualServiceConsultantWorkId) =>
        inContext.DeleteAsync<AnnualServiceConsultantWorkDataModel>(inAnnualServiceConsultantWorkId);

    public Task SaveAnnualServiceConsultantWorkAsync(AnnualServiceConsultantWork inAnnualServiceConsultantWork) =>
        inContext.SaveAsync<AnnualServiceConsultantWorkDataModel, AnnualServiceConsultantWork>(inAnnualServiceConsultantWork);

    public async Task<bool> IsAnnualServiceConsultantWorkUniqueAsync(AnnualServiceConsultantWork inReferenceAnnualServiceConsultantWork) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<AnnualServiceConsultantWorkDataModel>())
            .Where(inAnnualServiceConsultantWork => inAnnualServiceConsultantWork.Id != inReferenceAnnualServiceConsultantWork.Id)
            .AllAsync(inAnnualServiceConsultantWork =>
                inAnnualServiceConsultantWork.AnnualServiceId != inReferenceAnnualServiceConsultantWork.AnnualService.Id ||
                inAnnualServiceConsultantWork.ConsultantId != inReferenceAnnualServiceConsultantWork.Consultant.Id ||
                inAnnualServiceConsultantWork.SchoolYearId != inReferenceAnnualServiceConsultantWork.SchoolYear.Id);

    public Task<bool> CanAnnualServiceConsultantWorkBeDeletedAsync(Guid inAnnualServiceConsultantWorkId) =>
        inContext.CanBeDeleted<AnnualServiceConsultantWorkDataModel>(inAnnualServiceConsultantWorkId);
}