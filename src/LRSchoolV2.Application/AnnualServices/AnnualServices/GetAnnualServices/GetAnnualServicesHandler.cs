using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.GetAnnualServices;

public class GetAnnualServicesHandler(IAnnualServicesRepository inAnnualServicesRepository) : IRequestHandler<GetAnnualServicesQuery, GetAnnualServicesResponse>
{
    public async Task<GetAnnualServicesResponse> Handle(GetAnnualServicesQuery inRequest, CancellationToken inCancellationToken) =>
        new(await inAnnualServicesRepository.GetAnnualServicesAsync());
}