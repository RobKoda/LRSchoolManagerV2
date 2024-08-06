using LRSchoolV2.Domain.CheckDeposits;

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.GetCheckDeposits;

public record GetCheckDepositsResponse(IEnumerable<CheckDeposit> CheckDeposits);
