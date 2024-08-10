using LRSchoolV2.Domain.CustomerQuotes;

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.GetCustomerQuotes;

public record GetCustomerQuotesResponse(IEnumerable<CustomerQuote> CustomerQuotes);