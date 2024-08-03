using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.DeleteAnnualServiceConsultantWork;
using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.GetAnnualServiceConsultantWorksPerAnnualService;
using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.SaveAnnualServiceConsultantWork;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.AnnualServices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceConsultantWorks;

public class AnnualServiceConsultantWorksService(
    ISender inMediator,
    IValidator<DeleteAnnualServiceConsultantWorkRequest> inDeleteAnnualServiceConsultantWorkRequestValidator,
    IValidator<GetAnnualServiceConsultantWorksPerAnnualServiceRequest> inGetAnnualServiceConsultantWorksPerServiceRequestValidator,
    IValidator<SaveAnnualServiceConsultantWorkRequest> inSaveAnnualServiceConsultantWorkRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<AnnualServiceConsultantWork>>> GetAnnualServiceConsultantWorksPerServiceAsync(Guid inAnnualServiceId)
    {
        var request = new GetAnnualServiceConsultantWorksPerAnnualServiceRequest(inAnnualServiceId);
        var result = await inMediator.SendRequestWithValidation<GetAnnualServiceConsultantWorksPerAnnualServiceRequest, GetAnnualServiceConsultantWorksPerAnnualServiceQuery, GetAnnualServiceConsultantWorksPerAnnualServiceResponse>(request, inGetAnnualServiceConsultantWorksPerServiceRequestValidator);
        return result.Map<IEnumerable<AnnualServiceConsultantWork>>(inSuccess => inSuccess.AnnualServiceConsultantWorks);
    }

    public Task<Validation<string, Unit>> DeleteAnnualServiceConsultantWorkAsync(AnnualServiceConsultantWork inAnnualServiceConsultantWork)
    {
        var request = new DeleteAnnualServiceConsultantWorkRequest(inAnnualServiceConsultantWork);
        return inMediator.SendRequestWithValidation<DeleteAnnualServiceConsultantWorkRequest, DeleteAnnualServiceConsultantWorkCommand>(request, inDeleteAnnualServiceConsultantWorkRequestValidator);
    }

    public Task<Validation<string, Unit>> SaveAnnualServiceConsultantWorkAsync(AnnualServiceConsultantWork inAnnualServiceConsultantWork)
    {
        var request = new SaveAnnualServiceConsultantWorkRequest(inAnnualServiceConsultantWork);
        return inMediator.SendRequestWithValidation<SaveAnnualServiceConsultantWorkRequest, SaveAnnualServiceConsultantWorkCommand>(request, inSaveAnnualServiceConsultantWorkRequestValidator);
    }
}