using LRSchoolV2.Application.Common.Documents.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.Documents.GetDocumentPerReferenceId;

public class GetDocumentPerReferenceIdHandler(
    IDocumentsRepository inDocumentsRepository
    ) : IRequestHandler<GetDocumentPerReferenceIdQuery, GetDocumentPerReferenceIdResponse>
{
    public async Task<GetDocumentPerReferenceIdResponse> Handle(GetDocumentPerReferenceIdQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inDocumentsRepository.GetDocumentPerReferenceIdAsync(inRequest.ReferenceId));
}