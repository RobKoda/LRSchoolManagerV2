﻿using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;

public interface IAnnualServiceVariationConsultantWorksRepository : IRepository
{
    Task<IEnumerable<AnnualServiceVariationConsultantWork>> GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationAsync(Guid inAnnualServiceVariationId);
    Task<bool> AnyAnnualServiceVariationConsultantWorkAsync(Guid inAnnualServiceVariationConsultantWorkId);
    Task DeleteAnnualServiceVariationConsultantWorkAsync(Guid inAnnualServiceVariationConsultantWorkId);
    Task SaveAnnualServiceVariationConsultantWorkAsync(AnnualServiceVariationConsultantWork inAnnualServiceVariationConsultantWork);
    Task<bool> IsAnnualServiceVariationConsultantWorkUniqueAsync(AnnualServiceVariationConsultantWork inReferenceAnnualServiceVariationConsultantWork);
    Task<bool> CanAnnualServiceVariationConsultantWorkBeDeletedAsync(Guid inAnnualServiceVariationConsultantWorkId);
}