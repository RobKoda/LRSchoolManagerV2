using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Common.Documents.GetDocumentPerReferenceId;
using LRSchoolV2.Application.Common.Documents.SaveDocument;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.Documents;

public class DocumentsService(ISender inMediator, IValidator<SaveDocumentRequest> inSaveDocumentRequestValidator) : IFrontDataService
{
    public async Task<Option<Document>> GetDocumentPerReferenceIdAsync(Guid inReferenceId) => 
        (await inMediator.Send(new GetDocumentPerReferenceIdQuery(inReferenceId))).Document;

    public Task<Validation<string, Unit>> SaveDocumentAsync(Document inDocument)
    {
        var request = new SaveDocumentRequest(inDocument);
        return inMediator.SendRequestWithValidation<SaveDocumentRequest, SaveDocumentCommand>(request, inSaveDocumentRequestValidator);
    }
}