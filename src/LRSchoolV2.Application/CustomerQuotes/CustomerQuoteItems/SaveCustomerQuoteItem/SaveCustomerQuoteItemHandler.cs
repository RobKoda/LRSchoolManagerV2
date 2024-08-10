using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.SaveCustomerQuoteItem;

public class SaveCustomerQuoteItemHandler(
    ICustomerQuoteItemsRepository inCustomerQuoteItemsRepository
) : IRequestHandler<SaveCustomerQuoteItemCommand>
{
    public Task Handle(SaveCustomerQuoteItemCommand inRequest, CancellationToken inCancellationToken) => 
        inCustomerQuoteItemsRepository.SaveCustomerQuoteItemAsync(inRequest.CustomerQuoteItem);
}