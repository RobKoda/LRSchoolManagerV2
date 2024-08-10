using LRSchoolV2.Domain.CustomerQuotes;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.GetCustomerQuoteItemsPerCustomerQuote;

public record GetCustomerQuoteItemsPerCustomerQuoteResponse(IEnumerable<CustomerQuoteItem> CustomerQuoteItems);
