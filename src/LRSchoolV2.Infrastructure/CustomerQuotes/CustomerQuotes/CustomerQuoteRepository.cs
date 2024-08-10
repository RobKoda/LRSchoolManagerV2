using LanguageExt;
using static LanguageExt.Prelude;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using LRSchoolV2.Domain.CustomerQuotes;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;

public class CustomerQuotesRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerQuotesRepository
{
    public Task<Option<CustomerQuote>> GetCustomerQuoteAsync(Guid inCustomerQuoteId) =>
        inContext.GetSingleAsync<CustomerQuoteDataModel, CustomerQuote>(inCustomerQuoteId, GetCustomerQuoteQueryableAsync);
    
    public async Task<Option<CustomerQuote>> GetLastCustomerQuoteAsync() =>
        Optional(
            (await (await inContext.GetQueryableAsNoTrackingAsync<CustomerQuoteDataModel>())
                .OrderByDescending(inQuote => inQuote.Date)
                .FirstOrDefaultAsync())
            .Adapt<CustomerQuote>());
    
    public Task<IEnumerable<CustomerQuote>> GetCustomerQuotesAsync() =>
        inContext.GetAllAsync<CustomerQuoteDataModel, CustomerQuote>(GetCustomerQuoteQueryableAsync);

    public Task<bool> AnyCustomerQuoteAsync(Guid inId) =>
        inContext.AnyAsync<CustomerQuoteDataModel>(inId);

    public async Task<bool> CanCustomerQuoteBeDeletedAsync(Guid inId) =>
        await (await inContext.GetSingleAsync<CustomerQuoteDataModel>(inId))
            .MatchAsync(
                async inToValidate =>
                {
                    var context = await inContext.GetContextAsync();
                    await context.Database.BeginTransactionAsync();
                    
                    var document = await context.Documents.SingleOrDefaultAsync(inDocument => inDocument.ReferenceId == inToValidate.Id);
                    if (document != null)
                    {
                        context.Remove(document);
                    }
                    
                    var items = await context.CustomerQuoteItems
                        .Where(inItem => inItem.CustomerQuoteId == inToValidate.Id)
                        .ToListAsync();
                    context.RemoveRange(items);
                    
                    context.Remove(inToValidate);
                    try
                    {
                        await context.SaveChangesAsync();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        await context.Database.RollbackTransactionAsync();
                    }
                },
                () => false);
    
    public Task DeleteCustomerQuoteAsync(Guid inPersonId) =>
        inContext.DeleteAsync<CustomerQuoteDataModel>(inPersonId);
    
    public Task SaveCustomerQuoteAsync(CustomerQuote inPerson) =>
        inContext.SaveAsync<CustomerQuoteDataModel, CustomerQuote>(inPerson);
    
    private static IQueryable<CustomerQuoteDataModel> GetCustomerQuoteQueryableAsync(IQueryable<CustomerQuoteDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Customer)
            .Include(inPerson => inPerson.Items);
}