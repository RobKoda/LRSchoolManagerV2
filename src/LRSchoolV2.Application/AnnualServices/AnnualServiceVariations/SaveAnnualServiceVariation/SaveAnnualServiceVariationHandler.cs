using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.SaveAnnualServiceVariation;

public class SaveAnnualServiceVariationHandler : IRequestHandler<SaveAnnualServiceVariationCommand>
{
    private readonly IAnnualServiceVariationsRepository _annualServiceVariationsRepository;

    public SaveAnnualServiceVariationHandler(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository)
    {
        _annualServiceVariationsRepository = inAnnualServiceVariationsRepository;
    }

    public Task Handle(SaveAnnualServiceVariationCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceVariationsRepository.SaveAnnualServiceVariationAsync(inRequest.AnnualServiceVariation);
}