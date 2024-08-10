using LRSchoolV2.Application.Common.Documents.Persistence;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.Persistence;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;

public class CancelCustomerQuoteHandler(
    ICustomerQuotesRepository inCustomerQuotesRepository,
    ICustomerQuoteItemsRepository inCustomerQuoteItemsRepository,
    IDocumentsRepository inDocumentsRepository
    ) : IRequestHandler<CancelCustomerQuoteCommand>
{
    public async Task Handle(CancelCustomerQuoteCommand inRequest, CancellationToken inCancellationToken)
    {
        await inDocumentsRepository.DeleteDocumentPerReferenceIdAsync(inRequest.CustomerQuote.Id);
        
        var items = await inCustomerQuoteItemsRepository.GetCustomerQuoteItemsPerCustomerQuoteAsync(inRequest.CustomerQuote.Id);
        foreach (var item in items)
        {
            await inCustomerQuoteItemsRepository.DeleteCustomerQuoteItemAsync(item.Id);
        }
        await inCustomerQuotesRepository.DeleteCustomerQuoteAsync(inRequest.CustomerQuote.Id);
    }
}