using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.DeleteAnnualService;

public class DeleteAnnualServiceHandler : IRequestHandler<DeleteAnnualServiceCommand>
{
    private readonly IAnnualServicesRepository _annualServicesRepository;

    public DeleteAnnualServiceHandler(IAnnualServicesRepository inAnnualServicesRepository)
    {
        _annualServicesRepository = inAnnualServicesRepository;
    }

    public Task Handle(DeleteAnnualServiceCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServicesRepository.DeleteAnnualServiceAsync(inRequest.AnnualService.Id);
}