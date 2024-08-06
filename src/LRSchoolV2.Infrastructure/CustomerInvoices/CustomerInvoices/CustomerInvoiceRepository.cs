using LanguageExt;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using LRSchoolV2.Domain.CustomerInvoices;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;

public class CustomerInvoicesRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerInvoicesRepository
{
    public Task<Option<CustomerInvoice>> GetCustomerInvoiceAsync(Guid inCustomerInvoiceId) =>
        inContext.GetSingleAsync<CustomerInvoiceDataModel, CustomerInvoice>(inCustomerInvoiceId, GetCustomerInvoiceQueryableAsync);

    public Task<IEnumerable<CustomerInvoice>> GetCustomerInvoicesAsync() =>
        inContext.GetAllAsync<CustomerInvoiceDataModel, CustomerInvoice>(GetCustomerInvoiceQueryableAsync);

    public Task<bool> AnyCustomerInvoiceAsync(Guid inId) =>
        inContext.AnyAsync<CustomerInvoiceDataModel>(inId);

    public Task<bool> CanCustomerInvoiceBeDeletedAsync(Guid inId) =>
        inContext.CanBeDeleted<CustomerInvoiceDataModel>(inId);
    
    public Task DeleteCustomerInvoiceAsync(Guid inPersonId) =>
        inContext.DeleteAsync<CustomerInvoiceDataModel>(inPersonId);
    
    public Task SaveCustomerInvoiceAsync(CustomerInvoice inPerson) =>
        inContext.SaveAsync<CustomerInvoiceDataModel, CustomerInvoice>(inPerson);
    
    private static IQueryable<CustomerInvoiceDataModel> GetCustomerInvoiceQueryableAsync(IQueryable<CustomerInvoiceDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Customer)
            .Include(inPerson => inPerson.Items);
}