using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.GetCheckDepositPaymentsPerCheckDeposit;

public record GetCheckDepositPaymentsPerCheckDepositQuery(Guid CheckDepositId) : IRequest<GetCheckDepositPaymentsPerCheckDepositResponse>;