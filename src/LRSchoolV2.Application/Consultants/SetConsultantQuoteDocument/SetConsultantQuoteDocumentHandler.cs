using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.SetConsultantQuoteDocument;

public class SetConsultantQuoteDocumentHandler(IConsultantsRepository inConsultantsRepository) : IRequestHandler<SetConsultantQuoteDocumentCommand>
{
    public Task Handle(SetConsultantQuoteDocumentCommand inRequest, CancellationToken inCancellationToken) => 
        inConsultantsRepository.SetConsultantQuoteDocument(inRequest.ConsultantId, inRequest.QuoteDocument);
}