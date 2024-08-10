using LRSchoolV2.Application.Common.Documents.Persistence;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.Persistence;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;

public class CancelCustomerQuoteHandler(
    ICustomerQuotesRepository inCustomerQuotesRepository,
    ICustomerQuoteItemsRepository inCustomerQuoteItemsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IDocumentsRepository inDocumentsRepository
    ) : IRequestHandler<CancelCustomerQuoteCommand>
{
    public async Task Handle(CancelCustomerQuoteCommand inRequest, CancellationToken inCancellationToken)
    {
        await inDocumentsRepository.DeleteDocumentPerReferenceIdAsync(inRequest.CustomerQuote.Id);
        
        var items = await inCustomerQuoteItemsRepository.GetCustomerQuoteItemsPerCustomerQuoteAsync(inRequest.CustomerQuote.Id);
        foreach (var item in items)
        {
            await item.ReferenceId.IfSomeAsync(async inReferenceId =>
            {
                await inPersonRegistrationsRepository.SetFullyBilledAsync(new[] { inReferenceId }, false);
                await inPersonAnnualServiceVariationsRepository.SetFullyBilledAsync(new[] { inReferenceId }, false);
            });
            
            await inCustomerQuoteItemsRepository.DeleteCustomerQuoteItemAsync(item.Id);
        }
        await inCustomerQuotesRepository.DeleteCustomerQuoteAsync(inRequest.CustomerQuote.Id);
    }
}