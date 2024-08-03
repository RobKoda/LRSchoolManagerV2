using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Consultants.Consultants.DeleteConsultant;
using LRSchoolV2.Application.Consultants.Consultants.GetConsultants;
using LRSchoolV2.Application.Consultants.Consultants.SaveConsultant;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.Consultants;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.Consultants;

public class ConsultantsService(
    ISender inMediator,
    IValidator<DeleteConsultantRequest> inDeleteConsultantRequestValidator
    ) : IFrontDataService
{
    public async Task<IEnumerable<Consultant>> GetConsultantsAsync() => 
        (await inMediator.Send(new GetConsultantsQuery())).Consultants;

    public Task<Validation<string, Unit>> DeleteConsultantAsync(Consultant inConsultant)
    {
        var request = new DeleteConsultantRequest(inConsultant);
        return inMediator.SendRequestWithValidation<DeleteConsultantRequest, DeleteConsultantCommand>(request, inDeleteConsultantRequestValidator);
    }

    public Task SaveConsultantAsync(Consultant inConsultant) => 
        inMediator.Send(new SaveConsultantCommand(inConsultant));
}