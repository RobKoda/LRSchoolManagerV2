using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CustomerPayments;

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;

public interface ICustomerPaymentsRepository : IRepository
{
    Task<bool> AnyCustomerPaymentAsync(Guid inCustomerPaymentId);
    Task<IEnumerable<CustomerPayment>> GetUnlinkedChecksAsync();
    Task SaveCustomerPaymentAsync(CustomerPayment inRequestCustomerPayment);
    Task DeleteCustomerPaymentAsync(Guid inCustomerPaymentId);
    Task<bool> CanCustomerPaymentBeDeleted(Guid inCustomerPaymentId);
    Task<IEnumerable<CustomerPayment>> GetCustomerPaymentsAsync();
}