using LanguageExt;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using LRSchoolV2.Domain.CustomerQuotes;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;

public class CustomerQuotesRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerQuotesRepository
{
    public Task<Option<CustomerQuote>> GetCustomerQuoteAsync(Guid inCustomerQuoteId) =>
        inContext.GetSingleAsync<CustomerQuoteDataModel, CustomerQuote>(inCustomerQuoteId, GetCustomerQuoteQueryableAsync);

    public Task<IEnumerable<CustomerQuote>> GetCustomerQuotesAsync() =>
        inContext.GetAllAsync<CustomerQuoteDataModel, CustomerQuote>(GetCustomerQuoteQueryableAsync);

    public Task<bool> AnyCustomerQuoteAsync(Guid inId) =>
        inContext.AnyAsync<CustomerQuoteDataModel>(inId);

    public Task<bool> CanCustomerQuoteBeDeletedAsync(Guid inId) =>
        inContext.CanBeDeleted<CustomerQuoteDataModel>(inId);
    
    public Task DeleteCustomerQuoteAsync(Guid inPersonId) =>
        inContext.DeleteAsync<CustomerQuoteDataModel>(inPersonId);
    
    public Task SaveCustomerQuoteAsync(CustomerQuote inPerson) =>
        inContext.SaveAsync<CustomerQuoteDataModel, CustomerQuote>(inPerson);
    
    private static IQueryable<CustomerQuoteDataModel> GetCustomerQuoteQueryableAsync(IQueryable<CustomerQuoteDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Customer)
            .Include(inPerson => inPerson.Items);
}