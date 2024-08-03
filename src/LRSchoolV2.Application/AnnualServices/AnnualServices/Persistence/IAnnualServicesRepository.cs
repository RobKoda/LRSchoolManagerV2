using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;

public interface IAnnualServicesRepository : IRepository
{
    Task<IEnumerable<AnnualService>> GetAnnualServicesAsync();
    Task<bool> AnyAnnualServiceAsync(Guid inAnnualServiceId);
    Task DeleteAnnualServiceAsync(Guid inAnnualServiceId);
    Task SaveAnnualServiceAsync(AnnualService inAnnualService);
    Task<bool> CanAnnualServiceBeDeletedAsync(Guid inAnnualServiceId);
    Task<bool> IsAnnualServiceUniqueAsync(AnnualService inReferenceAnnualService);
}