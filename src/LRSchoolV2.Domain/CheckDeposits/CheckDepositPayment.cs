// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LRSchoolV2.Domain.CustomerPayments;

namespace LRSchoolV2.Domain.CheckDeposits;

public record CheckDepositPayment(
    Guid Id, 
    Guid CheckDepositId,
    CustomerPayment CustomerPayment
);