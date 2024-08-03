using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.SaveAnnualServiceConsultantWork;

public class SaveAnnualServiceConsultantWorkHandler : IRequestHandler<SaveAnnualServiceConsultantWorkCommand>
{
    private readonly IAnnualServiceConsultantWorksRepository _annualServiceConsultantWorksRepository;

    public SaveAnnualServiceConsultantWorkHandler(IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository)
    {
        _annualServiceConsultantWorksRepository = inAnnualServiceConsultantWorksRepository;
    }

    public Task Handle(SaveAnnualServiceConsultantWorkCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceConsultantWorksRepository.SaveAnnualServiceConsultantWorkAsync(inRequest.AnnualServiceConsultantWork);
}