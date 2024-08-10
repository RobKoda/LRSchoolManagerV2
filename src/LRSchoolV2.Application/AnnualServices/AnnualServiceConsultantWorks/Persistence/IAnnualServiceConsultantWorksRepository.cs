using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;

public interface IAnnualServiceConsultantWorksRepository : IRepository
{
    Task<IEnumerable<AnnualServiceConsultantWork>> GetAnnualServiceConsultantWorksPerAnnualServiceAsync(Guid inAnnualServiceId);
    Task<bool> AnyAnnualServiceConsultantWorkAsync(Guid inAnnualServiceConsultantWorkId);
    Task DeleteAnnualServiceConsultantWorkAsync(Guid inAnnualServiceConsultantWorkId);
    Task SaveAnnualServiceConsultantWorkAsync(AnnualServiceConsultantWork inAnnualServiceConsultantWork);
    Task<bool> IsAnnualServiceConsultantWorkUniqueAsync(AnnualServiceConsultantWork inReferenceAnnualServiceConsultantWork);
    Task<bool> CanAnnualServiceConsultantWorkBeDeletedAsync(Guid inAnnualServiceConsultantWorkId);
}