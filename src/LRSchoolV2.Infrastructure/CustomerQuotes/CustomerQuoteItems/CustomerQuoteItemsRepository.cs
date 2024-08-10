using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.Persistence;
using LRSchoolV2.Domain.CustomerQuotes;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems;

public class CustomerQuoteItemsRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerQuoteItemsRepository
{
    public Task<IEnumerable<CustomerQuoteItem>> GetCustomerQuoteItemsPerCustomerQuoteAsync(Guid inCustomerQuoteId) =>
        inContext.GetAllAsync<CustomerQuoteItemDataModel, CustomerQuoteItem>(inQueryable => GetCustomerQuoteItemQueryableAsync(inQueryable)
            .Where(inQuoteItem => inQuoteItem.CustomerQuoteId == inCustomerQuoteId)
        );

    public Task DeleteCustomerQuoteItemAsync(Guid inCustomerQuoteItemId) =>
        inContext.DeleteAsync<CustomerQuoteItemDataModel>(inCustomerQuoteItemId);

    public Task SaveCustomerQuoteItemAsync(CustomerQuoteItem inPerson) =>
        inContext.SaveAsync<CustomerQuoteItemDataModel, CustomerQuoteItem>(inPerson);

    private static IQueryable<CustomerQuoteItemDataModel> GetCustomerQuoteItemQueryableAsync(IQueryable<CustomerQuoteItemDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.CustomerQuote);
}