using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.DeleteAnnualServiceConsultantWork;

public class DeleteAnnualServiceConsultantWorkHandler : IRequestHandler<DeleteAnnualServiceConsultantWorkCommand>
{
    private readonly IAnnualServiceConsultantWorksRepository _annualServiceConsultantWorksRepository;

    public DeleteAnnualServiceConsultantWorkHandler(IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository)
    {
        _annualServiceConsultantWorksRepository = inAnnualServiceConsultantWorksRepository;
    }

    public Task Handle(DeleteAnnualServiceConsultantWorkCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceConsultantWorksRepository.DeleteAnnualServiceConsultantWorkAsync(inRequest.AnnualServiceConsultantWork.Id);
}