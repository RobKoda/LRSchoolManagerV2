using LRSchoolV2.Application.AnnualServices.AnnualServices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.SaveAnnualService;

public class SaveAnnualServiceHandler : IRequestHandler<SaveAnnualServiceCommand>
{
    private readonly IAnnualServicesRepository _annualServicesRepository;

    public SaveAnnualServiceHandler(IAnnualServicesRepository inAnnualServicesRepository)
    {
        _annualServicesRepository = inAnnualServicesRepository;
    }

    public Task Handle(SaveAnnualServiceCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServicesRepository.SaveAnnualServiceAsync(inRequest.AnnualService);
}