using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.AnnualServices.AnnualServices.DeleteAnnualService;
using LRSchoolV2.Application.AnnualServices.AnnualServices.GetAnnualServices;
using LRSchoolV2.Application.AnnualServices.AnnualServices.SaveAnnualService;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.AnnualServices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.AnnualServices;

public class AnnualServicesService(
    ISender inMediator,
    IValidator<DeleteAnnualServiceRequest> inDeleteAnnualServiceRequestValidator,
    IValidator<SaveAnnualServiceRequest> inSaveAnnualServiceRequestValidator
    ) : IFrontDataService
{
    public async Task<IEnumerable<AnnualService>> GetAnnualServicesAsync() => 
        (await inMediator.Send(new GetAnnualServicesQuery())).AnnualServices;

    public Task<Validation<string, Unit>> DeleteAnnualServiceAsync(AnnualService inAnnualService)
    {
        var request = new DeleteAnnualServiceRequest(inAnnualService);
        return inMediator.SendRequestWithValidation<DeleteAnnualServiceRequest, DeleteAnnualServiceCommand>(request, inDeleteAnnualServiceRequestValidator);
    }

    public Task<Validation<string, Unit>> SaveAnnualServiceAsync(AnnualService inAnnualService)
    {
        var request = new SaveAnnualServiceRequest(inAnnualService);
        return inMediator.SendRequestWithValidation<SaveAnnualServiceRequest, SaveAnnualServiceCommand>(request, inSaveAnnualServiceRequestValidator);
    }
}