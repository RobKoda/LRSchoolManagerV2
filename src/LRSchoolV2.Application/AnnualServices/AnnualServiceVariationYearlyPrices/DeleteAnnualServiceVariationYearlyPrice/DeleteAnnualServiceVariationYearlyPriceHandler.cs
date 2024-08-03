using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.DeleteAnnualServiceVariationYearlyPrice;

public class DeleteAnnualServiceVariationYearlyPriceHandler : IRequestHandler<DeleteAnnualServiceVariationYearlyPriceCommand>
{
    private readonly IAnnualServiceVariationYearlyPricesRepository _annualServiceVariationYearlyPricesRepository;

    public DeleteAnnualServiceVariationYearlyPriceHandler(IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository)
    {
        _annualServiceVariationYearlyPricesRepository = inAnnualServiceVariationYearlyPricesRepository;
    }

    public Task Handle(DeleteAnnualServiceVariationYearlyPriceCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceVariationYearlyPricesRepository.DeleteAnnualServiceVariationYearlyPriceAsync(inRequest.AnnualServiceVariationYearlyPrice.Id);
}