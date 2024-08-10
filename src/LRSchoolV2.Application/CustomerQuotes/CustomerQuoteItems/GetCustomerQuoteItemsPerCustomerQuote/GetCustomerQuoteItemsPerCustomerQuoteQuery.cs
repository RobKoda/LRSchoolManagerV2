using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.GetCustomerQuoteItemsPerCustomerQuote;

public record GetCustomerQuoteItemsPerCustomerQuoteQuery(Guid CustomerQuoteId) : IRequest<GetCustomerQuoteItemsPerCustomerQuoteResponse>;