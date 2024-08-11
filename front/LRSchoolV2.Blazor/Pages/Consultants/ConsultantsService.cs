using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Consultants.DeleteConsultant;
using LRSchoolV2.Application.Consultants.GetConsultantInvoiceDocument;
using LRSchoolV2.Application.Consultants.GetConsultantQuoteDocument;
using LRSchoolV2.Application.Consultants.GetConsultants;
using LRSchoolV2.Application.Consultants.SaveConsultant;
using LRSchoolV2.Application.Consultants.SetConsultantInvoiceDocument;
using LRSchoolV2.Application.Consultants.SetConsultantQuoteDocument;
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
    
    public Task SetConsultantQuoteDocumentAsync(Guid inConsultantId, byte[] inDocument) =>
        inMediator.Send(new SetConsultantQuoteDocumentCommand(inConsultantId, inDocument));
    
    public Task SetConsultantInvoiceDocumentAsync(Guid inConsultantId, byte[] inDocument) =>
        inMediator.Send(new SetConsultantInvoiceDocumentCommand(inConsultantId, inDocument));
    
    public async Task<Option<byte[]>> GetConsultantQuoteDocumentAsync(Guid inConsultantId) =>
        (await inMediator.Send(new GetConsultantQuoteDocumentQuery(inConsultantId))).ConsultantQuoteDocument;
    
    public async Task<Option<byte[]>> GetConsultantInvoiceDocumentAsync(Guid inConsultantId) =>
        (await inMediator.Send(new GetConsultantInvoiceDocumentQuery(inConsultantId))).ConsultantInvoiceDocument;
}