// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use

using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Domain.CustomerPayments;

public record CustomerPayment(
    Guid Id,
    Person Person,
    DateTime Date,
    int CustomerPaymentTypeValue,
    decimal Amount,
    string Reference);
