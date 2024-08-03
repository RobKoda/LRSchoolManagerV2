using LRSchoolV2.Application.Common.Documents.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.Documents.GetDocumentPerReferenceId;

public class GetDocumentPerReferenceIdHandler : IRequestHandler<GetDocumentPerReferenceIdQuery, GetDocumentPerReferenceIdResponse>
{
    private readonly IDocumentsRepository _documentsRepository;

    public GetDocumentPerReferenceIdHandler(IDocumentsRepository inDocumentsRepository)
    {
        _documentsRepository = inDocumentsRepository;
    }

    public async Task<GetDocumentPerReferenceIdResponse> Handle(GetDocumentPerReferenceIdQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _documentsRepository.GetDocumentPerReferenceIdAsync(inRequest.ReferenceId));
}