using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CustomerInvoices;

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.Persistence;

public interface ICustomerInvoiceItemsRepository : IRepository
{
    Task<IEnumerable<CustomerInvoiceItem>> GetCustomerInvoiceItemsPerCustomerInvoiceAsync(Guid inCustomerInvoiceId);
    Task SaveCustomerInvoiceItemAsync(CustomerInvoiceItem inCustomerInvoiceItem);
    Task DeleteCustomerInvoiceItemAsync(Guid inCustomerInvoiceItemId);
}