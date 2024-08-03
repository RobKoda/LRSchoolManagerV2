using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.SaveAnnualServiceVariationConsultantWork;

public class SaveAnnualServiceVariationConsultantWorkHandler : IRequestHandler<SaveAnnualServiceVariationConsultantWorkCommand>
{
    private readonly IAnnualServiceVariationConsultantWorksRepository _annualServiceVariationConsultantWorksRepository;

    public SaveAnnualServiceVariationConsultantWorkHandler(IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository)
    {
        _annualServiceVariationConsultantWorksRepository = inAnnualServiceVariationConsultantWorksRepository;
    }

    public Task Handle(SaveAnnualServiceVariationConsultantWorkCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceVariationConsultantWorksRepository.SaveAnnualServiceVariationConsultantWorkAsync(inRequest.AnnualServiceVariationConsultantWork);
}