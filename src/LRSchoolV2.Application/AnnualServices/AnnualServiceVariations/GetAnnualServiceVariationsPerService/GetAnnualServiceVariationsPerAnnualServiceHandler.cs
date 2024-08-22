using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.GetAnnualServiceVariationsPerService;

public class GetAnnualServiceVariationsPerAnnualServiceHandler(
    IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository,
    IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository,
    ISender inMediator)
    : IRequestHandler<GetAnnualServiceVariationsPerAnnualServiceQuery, GetAnnualServiceVariationsPerAnnualServiceResponse>
{
    public async Task<GetAnnualServiceVariationsPerAnnualServiceResponse> Handle(GetAnnualServiceVariationsPerAnnualServiceQuery inRequest, CancellationToken inCancellationToken) =>
        new ((await inMediator.Send(new GetCurrentSchoolYearQuery(), inCancellationToken)).SchoolYear
            .MapAsync<SchoolYear, IEnumerable<AnnualServiceVariation>>(async inSome =>
            {
                var currentYearlyPrices = await inAnnualServiceVariationYearlyPricesRepository.GetCurrentYearlyPricesAsync(inSome.Id);
                return (await inAnnualServiceVariationsRepository.GetAnnualServiceVariationsPerAnnualServiceAsync(inRequest.AnnualServiceId))
                    .Select(inServiceVariation => inServiceVariation with
                    {
                        CurrentYearlyPrice = currentYearlyPrices.SingleOrDefault(inYearlyPrice => inYearlyPrice.SchoolYear == inSome && inYearlyPrice.AnnualServiceVariation.Id == inServiceVariation.Id)
                    });
            }));
}