using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.DeleteAnnualServiceVariationConsultantWork;

public class DeleteAnnualServiceVariationConsultantWorkHandler : IRequestHandler<DeleteAnnualServiceVariationConsultantWorkCommand>
{
    private readonly IAnnualServiceVariationConsultantWorksRepository _annualServiceVariationConsultantWorksRepository;

    public DeleteAnnualServiceVariationConsultantWorkHandler(IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository)
    {
        _annualServiceVariationConsultantWorksRepository = inAnnualServiceVariationConsultantWorksRepository;
    }

    public Task Handle(DeleteAnnualServiceVariationConsultantWorkCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceVariationConsultantWorksRepository.DeleteAnnualServiceVariationConsultantWorkAsync(inRequest.AnnualServiceVariationConsultantWork.Id);
}