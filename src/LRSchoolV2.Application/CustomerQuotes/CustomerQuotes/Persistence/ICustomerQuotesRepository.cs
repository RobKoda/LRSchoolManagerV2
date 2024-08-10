using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CustomerQuotes;

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;

public interface ICustomerQuotesRepository : IRepository
{
    Task<Option<CustomerQuote>> GetCustomerQuoteAsync(Guid inCustomerQuoteId);
    Task<IEnumerable<CustomerQuote>> GetCustomerQuotesAsync();
    Task SaveCustomerQuoteAsync(CustomerQuote inCustomerQuote);
    Task<bool> AnyCustomerQuoteAsync(Guid inContactCustomerQuoteId);
    Task<bool> CanCustomerQuoteBeDeletedAsync(Guid inContactCustomerQuoteId);
    Task DeleteCustomerQuoteAsync(Guid inCustomerQuoteId);
}