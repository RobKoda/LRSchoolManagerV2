using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using Microsoft.EntityFrameworkCore;

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;

public class AnnualServicesRepository(IDbContextFactory<ApplicationContext> inContext) : IAnnualServicesRepository
{
    public Task<IEnumerable<AnnualService>> GetAnnualServicesAsync() =>
        inContext.GetAllAsync<AnnualServiceDataModel, AnnualService>();

    public Task<bool> AnyAnnualServiceAsync(Guid inAnnualServiceId) =>
        inContext.AnyAsync<AnnualServiceDataModel>(inAnnualServiceId);

    public Task DeleteAnnualServiceAsync(Guid inAnnualServiceId) =>
        inContext.DeleteAsync<AnnualServiceDataModel>(inAnnualServiceId);

    public Task SaveAnnualServiceAsync(AnnualService inAnnualService) =>
        inContext.SaveAsync<AnnualServiceDataModel, AnnualService>(inAnnualService);

    public Task<bool> CanAnnualServiceBeDeletedAsync(Guid inAnnualServiceId) => 
        inContext.CanBeDeleted<AnnualServiceDataModel>(inAnnualServiceId);

    public async Task<bool>  IsAnnualServiceUniqueAsync(AnnualService inReferenceAnnualService) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<AnnualServiceDataModel>())
            .Where(inAnnualService => inAnnualService.Id != inReferenceAnnualService.Id)
            .AllAsync(inAnnualService => !string.Equals(inAnnualService.Name.ToUpper(), inReferenceAnnualService.Name.ToUpper()));
}
