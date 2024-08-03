using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.GetAnnualServiceConsultantWorksPerAnnualService;

public class GetAnnualServiceConsultantWorksPerAnnualServiceHandler : IRequestHandler<GetAnnualServiceConsultantWorksPerAnnualServiceQuery, GetAnnualServiceConsultantWorksPerAnnualServiceResponse>
{
    private readonly IAnnualServiceConsultantWorksRepository _annualServiceConsultantWorksRepository;

    public GetAnnualServiceConsultantWorksPerAnnualServiceHandler(IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository)
    {
        _annualServiceConsultantWorksRepository = inAnnualServiceConsultantWorksRepository;
    }

    public async Task<GetAnnualServiceConsultantWorksPerAnnualServiceResponse> Handle(GetAnnualServiceConsultantWorksPerAnnualServiceQuery inRequest, CancellationToken inCancellationToken) =>
        new(await _annualServiceConsultantWorksRepository.GetAnnualServiceConsultantWorksPerAnnualServiceAsync(inRequest.AnnualServiceId));
}