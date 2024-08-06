using LRSchoolV2.Domain.CheckDeposits;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;

public record SaveCheckDepositPaymentCommand(CheckDepositPayment CheckDepositPayment) : IRequest;
