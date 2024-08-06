using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CheckDeposits;

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;

public interface ICheckDepositPaymentsRepository : IRepository
{
    Task SaveCheckDepositPaymentAsync(CheckDepositPayment inCheckDepositPayment);
    Task DeleteCheckDepositPaymentAsync(Guid inCheckDepositPaymentId);
    Task<IEnumerable<CheckDepositPayment>> GetCheckDepositPaymentsPerCheckDepositAsync(Guid inCheckDepositId);
    Task<bool> AnyCheckDepositPaymentAsync(Guid inCheckDepositPaymentId);
    Task<bool> AnyCheckDepositPaymentPerCheckIdAsync(Guid inCheckId);
    Task<bool> CanCheckDepositPaymentBeDeletedAsync(Guid inCheckDepositPaymentId);
}