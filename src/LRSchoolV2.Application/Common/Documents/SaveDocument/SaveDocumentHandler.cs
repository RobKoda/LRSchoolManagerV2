using LRSchoolV2.Application.Common.Documents.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.Documents.SaveDocument;

public class SaveDocumentHandler(IDocumentsRepository inDocumentsRepository) : IRequestHandler<SaveDocumentCommand>
{
    public Task Handle(SaveDocumentCommand inRequest, CancellationToken inCancellationToken) => 
        inDocumentsRepository.SaveDocumentAsync(inRequest.Document);
}