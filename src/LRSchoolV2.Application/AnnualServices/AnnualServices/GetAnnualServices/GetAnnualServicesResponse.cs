using LRSchoolV2.Domain.AnnualServices;

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.GetAnnualServices;

public record GetAnnualServicesResponse(IEnumerable<AnnualService> AnnualServices);
