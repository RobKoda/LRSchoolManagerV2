using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.DeleteAnnualServiceVariationConsultantWork;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariation;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.SaveAnnualServiceVariationConsultantWork;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.AnnualServices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.AnnualServiceVariationConsultantWorks;

public class AnnualServiceVariationConsultantWorksService(
    ISender inMediator,
    IValidator<DeleteAnnualServiceVariationConsultantWorkRequest> inDeleteAnnualServiceVariationConsultantWorkRequestValidator,
    IValidator<GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequest> inGetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequestValidator,
    IValidator<SaveAnnualServiceVariationConsultantWorkRequest> inSaveAnnualServiceVariationConsultantWorkRequestValidator)
{
    public async Task<Validation<string, IEnumerable<AnnualServiceVariationConsultantWork>>> GetAnnualServiceVariationConsultantWorksPerAnnualServiceAsync(Guid inServiceId)
    {
        var request = new GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequest(inServiceId);
        var result = await inMediator.SendRequestWithValidation<GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequest, GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationQuery, GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationResponse>(request, inGetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationRequestValidator);
        return result.Map<IEnumerable<AnnualServiceVariationConsultantWork>>(inSuccess => inSuccess.AnnualServiceVariationConsultantWorks);
    }

    public Task<Validation<string, Unit>> DeleteAnnualServiceVariationConsultantWorkAsync(AnnualServiceVariationConsultantWork inAnnualServiceVariationConsultantWork)
    {
        var request = new DeleteAnnualServiceVariationConsultantWorkRequest(inAnnualServiceVariationConsultantWork);
        return inMediator.SendRequestWithValidation<DeleteAnnualServiceVariationConsultantWorkRequest, DeleteAnnualServiceVariationConsultantWorkCommand>(request, inDeleteAnnualServiceVariationConsultantWorkRequestValidator);
    }

    public Task<Validation<string, Unit>> SaveAnnualServiceVariationConsultantWorkAsync(AnnualServiceVariationConsultantWork inAnnualServiceVariationConsultantWork)
    {
        var request = new SaveAnnualServiceVariationConsultantWorkRequest(inAnnualServiceVariationConsultantWork);
        return inMediator.SendRequestWithValidation<SaveAnnualServiceVariationConsultantWorkRequest, SaveAnnualServiceVariationConsultantWorkCommand>(request, inSaveAnnualServiceVariationConsultantWorkRequestValidator);
    }
}