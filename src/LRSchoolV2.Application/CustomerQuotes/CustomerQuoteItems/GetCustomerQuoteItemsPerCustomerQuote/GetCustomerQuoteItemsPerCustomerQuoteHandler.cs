using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.GetCustomerQuoteItemsPerCustomerQuote;

public class GetCustomerQuoteItemsPerCustomerQuoteHandler(
    ICustomerQuoteItemsRepository inCustomerQuoteItemsRepository
) : IRequestHandler<GetCustomerQuoteItemsPerCustomerQuoteQuery, GetCustomerQuoteItemsPerCustomerQuoteResponse>
{
    public async Task<GetCustomerQuoteItemsPerCustomerQuoteResponse> Handle(GetCustomerQuoteItemsPerCustomerQuoteQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inCustomerQuoteItemsRepository.GetCustomerQuoteItemsPerCustomerQuoteAsync(inRequest.CustomerQuoteId));
}