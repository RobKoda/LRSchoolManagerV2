using LanguageExt;
using static LanguageExt.Prelude;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using LRSchoolV2.Domain.CustomerInvoices;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;

public class CustomerInvoicesRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerInvoicesRepository
{
    public Task<Option<CustomerInvoice>> GetCustomerInvoiceAsync(Guid inCustomerInvoiceId) =>
        inContext.GetSingleAsync<CustomerInvoiceDataModel, CustomerInvoice>(inCustomerInvoiceId, GetCustomerInvoiceQueryableAsync);
    
    public async Task<Option<CustomerInvoice>> GetLastCustomerInvoiceAsync() =>
        Optional(
            (await (await inContext.GetQueryableAsNoTrackingAsync<CustomerInvoiceDataModel>())
                .OrderByDescending(inQuote => inQuote.Date)
                .FirstOrDefaultAsync())
            .Adapt<CustomerInvoice>());

    public Task<IEnumerable<CustomerInvoice>> GetCustomerInvoicesAsync() =>
        inContext.GetAllAsync<CustomerInvoiceDataModel, CustomerInvoice>(GetCustomerInvoiceQueryableAsync);

    public Task<bool> AnyCustomerInvoiceAsync(Guid inId) =>
        inContext.AnyAsync<CustomerInvoiceDataModel>(inId);

    public async Task<bool> CanCustomerInvoiceBeDeletedAsync(Guid inId) =>
        await (await inContext.GetSingleAsync<CustomerInvoiceDataModel>(inId))
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
                    
                    var items = await context.CustomerInvoiceItems
                        .Where(inItem => inItem.CustomerInvoiceId == inToValidate.Id)
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
    
    public Task DeleteCustomerInvoiceAsync(Guid inPersonId) =>
        inContext.DeleteAsync<CustomerInvoiceDataModel>(inPersonId);
    
    public Task SaveCustomerInvoiceAsync(CustomerInvoice inPerson) =>
        inContext.SaveAsync<CustomerInvoiceDataModel, CustomerInvoice>(inPerson);
    
    private static IQueryable<CustomerInvoiceDataModel> GetCustomerInvoiceQueryableAsync(IQueryable<CustomerInvoiceDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Customer)
            .Include(inPerson => inPerson.Items);
}