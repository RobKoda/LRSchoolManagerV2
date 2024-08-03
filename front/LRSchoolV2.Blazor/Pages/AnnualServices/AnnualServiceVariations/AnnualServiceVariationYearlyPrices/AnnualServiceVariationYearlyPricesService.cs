using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.DeleteAnnualServiceVariationYearlyPrice;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariation;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.SaveAnnualServiceVariationYearlyPrice;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.AnnualServiceVariationYearlyPrices;

public class AnnualServiceVariationYearlyPricesService(
    ISender inMediator,
    IValidator<DeleteAnnualServiceVariationYearlyPriceRequest> inDeleteAnnualServiceVariationYearlyPriceRequestValidator,
    IValidator<GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequest> inGetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequestValidator,
    IValidator<SaveAnnualServiceVariationYearlyPriceRequest> inSaveAnnualServiceVariationYearlyPriceRequestValidator)
{
    public async Task<Validation<string, IEnumerable<AnnualServiceVariationYearlyPrice>>> GetAnnualServiceVariationYearlyPricesPerAnnualServiceAsync(Guid inServiceId)
    {
        var request = new GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequest(inServiceId);
        var result = await inMediator.SendRequestWithValidation<GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequest, GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationQuery, GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationResponse>(request, inGetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationRequestValidator);
        return result.Map<IEnumerable<AnnualServiceVariationYearlyPrice>>(inSuccess => inSuccess.ServiceVariationYearlyPrices);
    }

    public Task<Validation<string, Unit>> DeleteAnnualServiceVariationYearlyPriceAsync(AnnualServiceVariationYearlyPrice inAnnualServiceVariationYearlyPrice)
    {
        var request = new DeleteAnnualServiceVariationYearlyPriceRequest(inAnnualServiceVariationYearlyPrice);
        return inMediator.SendRequestWithValidation<DeleteAnnualServiceVariationYearlyPriceRequest, DeleteAnnualServiceVariationYearlyPriceCommand>(request, inDeleteAnnualServiceVariationYearlyPriceRequestValidator);
    }

    public Task<Validation<string, Unit>> SaveAnnualServiceVariationYearlyPriceAsync(AnnualServiceVariationYearlyPrice inAnnualServiceVariationYearlyPrice)
    {
        var request = new SaveAnnualServiceVariationYearlyPriceRequest(inAnnualServiceVariationYearlyPrice);
        return inMediator.SendRequestWithValidation<SaveAnnualServiceVariationYearlyPriceRequest, SaveAnnualServiceVariationYearlyPriceCommand>(request, inSaveAnnualServiceVariationYearlyPriceRequestValidator);
    }
}