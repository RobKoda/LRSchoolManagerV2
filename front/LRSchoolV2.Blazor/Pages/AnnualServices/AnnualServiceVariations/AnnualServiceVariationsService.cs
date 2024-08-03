using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.DeleteAnnualServiceVariation;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.GetAnnualServiceVariationsPerService;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.GetCurrentAnnualServiceVariations;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.SaveAnnualServiceVariation;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.AnnualServices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations;

public class AnnualServiceVariationsService(
    ISender inMediator,
    IValidator<DeleteAnnualServiceVariationRequest> inDeleteAnnualServiceVariationRequestValidator,
    IValidator<GetAnnualServiceVariationsPerServiceRequest> inGetAnnualServiceVariationsPerServiceRequestValidator,
    IValidator<SaveAnnualServiceVariationRequest> inSaveAnnualServiceVariationRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, OptionAsync<IEnumerable<AnnualServiceVariation>>>> GetAnnualServiceVariationsPerServiceAsync(Guid inServiceId)
    {
        var request = new GetAnnualServiceVariationsPerServiceRequest(inServiceId);
        var result = await inMediator.SendRequestWithValidation<GetAnnualServiceVariationsPerServiceRequest, GetAnnualServiceVariationsPerAnnualServiceQuery, GetAnnualServiceVariationsPerAnnualServiceResponse>(request, inGetAnnualServiceVariationsPerServiceRequestValidator);
        return result.Map(inSuccess => inSuccess.AnnualServiceVariations);
    }

    public Task<Validation<string, Unit>> DeleteAnnualServiceVariationAsync(AnnualServiceVariation inAnnualServiceVariation)
    {
        var request = new DeleteAnnualServiceVariationRequest(inAnnualServiceVariation);
        return inMediator.SendRequestWithValidation<DeleteAnnualServiceVariationRequest, DeleteAnnualServiceVariationCommand>(request, inDeleteAnnualServiceVariationRequestValidator);
    }

    public Task<Validation<string, Unit>> SaveAnnualServiceVariationAsync(AnnualServiceVariation inAnnualServiceVariation)
    {
        var request = new SaveAnnualServiceVariationRequest(inAnnualServiceVariation);
        return inMediator.SendRequestWithValidation<SaveAnnualServiceVariationRequest, SaveAnnualServiceVariationCommand>(request, inSaveAnnualServiceVariationRequestValidator);
    }

    public async Task<OptionAsync<IEnumerable<AnnualServiceVariation>>> GetCurrentAnnualServiceVariationsAsync() =>
        (await inMediator.Send(new GetCurrentAnnualServiceVariationsQuery())).AnnualServiceVariations;
}