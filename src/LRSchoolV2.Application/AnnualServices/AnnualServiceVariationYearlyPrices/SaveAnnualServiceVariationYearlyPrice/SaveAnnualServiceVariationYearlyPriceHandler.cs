using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.SaveAnnualServiceVariationYearlyPrice;

public class SaveAnnualServiceVariationYearlyPriceHandler : IRequestHandler<SaveAnnualServiceVariationYearlyPriceCommand>
{
    private readonly IAnnualServiceVariationYearlyPricesRepository _annualServiceVariationYearlyPricesRepository;

    public SaveAnnualServiceVariationYearlyPriceHandler(IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository)
    {
        _annualServiceVariationYearlyPricesRepository = inAnnualServiceVariationYearlyPricesRepository;
    }

    public Task Handle(SaveAnnualServiceVariationYearlyPriceCommand inRequest, CancellationToken inCancellationToken) => 
        _annualServiceVariationYearlyPricesRepository.SaveAnnualServiceVariationYearlyPriceAsync(inRequest.AnnualServiceVariationYearlyPrice);
}