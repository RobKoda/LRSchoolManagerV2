using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CustomerInvoices;

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;

public interface ICustomerInvoicesRepository : IRepository
{
    Task<Option<CustomerInvoice>> GetCustomerInvoiceAsync(Guid inCustomerInvoiceId);
    Task<Option<CustomerInvoice>> GetLastCustomerInvoiceAsync();
    Task<IEnumerable<CustomerInvoice>> GetCustomerInvoicesAsync();
    Task SaveCustomerInvoiceAsync(CustomerInvoice inCustomerInvoice);
    Task<bool> AnyCustomerInvoiceAsync(Guid inContactCustomerInvoiceId);
    Task<bool> CanCustomerInvoiceBeDeletedAsync(Guid inContactCustomerInvoiceId);
    Task DeleteCustomerInvoiceAsync(Guid inCustomerInvoiceId);
}