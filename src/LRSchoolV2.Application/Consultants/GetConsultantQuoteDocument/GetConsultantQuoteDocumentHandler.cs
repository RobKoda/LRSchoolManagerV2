using LanguageExt;
using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.GetConsultantQuoteDocument;

public class GetConsultantQuoteDocumentHandler(IConsultantsRepository inConsultantsRepository) : IRequestHandler<GetConsultantQuoteDocumentQuery, GetConsultantQuoteDocumentResponse>
{
    public async Task<GetConsultantQuoteDocumentResponse> Handle(GetConsultantQuoteDocumentQuery inRequest, CancellationToken inCancellationToken) =>
        new(await inConsultantsRepository
                .GetConsultantQuoteDocument(inRequest.ConsultantId)
                .Match(inSome => inSome.Length == 0 ? Option<byte[]>.None : inSome,
                    () => Option<byte[]>.None));
}