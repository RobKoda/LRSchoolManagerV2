using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.CheckDeposits;

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;

public interface ICheckDepositsRepository : IRepository
{
    Task SaveCheckDepositAsync(CheckDeposit inCheckDeposit);
    Task<bool> AnyCheckDepositAsync(Guid inCheckDepositId);
    Task<bool> CanCheckDepositBeDeleted(Guid inCheckDepositId);
    Task DeleteCheckDepositAsync(Guid inCheckDepositId);
    Task<IEnumerable<CheckDeposit>> GetCheckDepositsAsync();
}