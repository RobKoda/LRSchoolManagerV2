using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CustomerQuotes;

namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.Persistence;

public interface ICustomerQuoteItemsRepository : IRepository
{
    Task<IEnumerable<CustomerQuoteItem>> GetCustomerQuoteItemsPerCustomerQuoteAsync(Guid inCustomerQuoteId);
    Task SaveCustomerQuoteItemAsync(CustomerQuoteItem inCustomerQuoteItem);
    Task DeleteCustomerQuoteItemAsync(Guid inCustomerQuoteItemId);
}