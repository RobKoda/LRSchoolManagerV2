using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.Persistence;
using LRSchoolV2.Domain.CustomerInvoices;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;

public class CustomerInvoiceItemsRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerInvoiceItemsRepository
{
    public Task<IEnumerable<CustomerInvoiceItem>> GetCustomerInvoiceItemsPerCustomerInvoiceAsync(Guid inCustomerInvoiceId) =>
        inContext.GetAllAsync<CustomerInvoiceItemDataModel, CustomerInvoiceItem>(inQueryable => GetCustomerInvoiceItemQueryableAsync(inQueryable)
            .Where(inInvoiceItem => inInvoiceItem.CustomerInvoiceId == inCustomerInvoiceId)
        );

    public Task DeleteCustomerInvoiceItemAsync(Guid inCustomerInvoiceItemId) =>
        inContext.DeleteAsync<CustomerInvoiceItemDataModel>(inCustomerInvoiceItemId);

    public Task SaveCustomerInvoiceItemAsync(CustomerInvoiceItem inPerson) =>
        inContext.SaveAsync<CustomerInvoiceItemDataModel, CustomerInvoiceItem>(inPerson);

    private static IQueryable<CustomerInvoiceItemDataModel> GetCustomerInvoiceItemQueryableAsync(IQueryable<CustomerInvoiceItemDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.CustomerInvoice);
}