using LRSchoolV2.Application.Common.Documents.Persistence;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.Persistence;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.CancelConsultantQuote;

public class CancelConsultantQuoteHandler(
    IConsultantQuotesRepository inConsultantQuotesRepository,
    IConsultantQuoteItemsRepository inConsultantQuoteItemsRepository,
    IDocumentsRepository inDocumentsRepository
    ) : IRequestHandler<CancelConsultantQuoteCommand>
{
    public async Task Handle(CancelConsultantQuoteCommand inRequest, CancellationToken inCancellationToken)
    {
        await inDocumentsRepository.DeleteDocumentPerReferenceIdAsync(inRequest.ConsultantQuote.Id);
        
        var items = await inConsultantQuoteItemsRepository.GetConsultantQuoteItemsPerConsultantQuoteAsync(inRequest.ConsultantQuote.Id);
        foreach (var item in items)
        {
            await inConsultantQuoteItemsRepository.DeleteConsultantQuoteItemAsync(item.Id);
        }
        await inConsultantQuotesRepository.DeleteConsultantQuoteAsync(inRequest.ConsultantQuote.Id);
    }
}