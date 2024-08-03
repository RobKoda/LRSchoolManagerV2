using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.DeleteAnnualServiceVariation;

public class DeleteAnnualServiceVariationHandler : IRequestHandler<DeleteAnnualServiceVariationCommand>
{
    private readonly IAnnualServiceVariationsRepository _annualServiceVariationsRepository;

    public DeleteAnnualServiceVariationHandler(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository)
    {
        _annualServiceVariationsRepository = inAnnualServiceVariationsRepository;
    }

    public Task Handle(DeleteAnnualServiceVariationCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceVariationsRepository.DeleteAnnualServiceVariationAsync(inRequest.AnnualServiceVariation.Id);
}