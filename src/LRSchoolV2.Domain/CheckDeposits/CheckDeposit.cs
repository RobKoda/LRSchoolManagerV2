// ReSharper disable ClassNeverInstantiated.Global - Implicit use
// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.CheckDeposits;

public record CheckDeposit(
    Guid Id, 
    DateTime Date, 
    string Number, 
    decimal Total, 
    bool HasDocument);