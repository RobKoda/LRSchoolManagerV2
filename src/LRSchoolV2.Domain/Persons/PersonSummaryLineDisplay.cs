// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Persons;

public record PersonSummaryLineDisplay(
    string Date,
    string Type,
    string Reference,
    string Debit,
    string Credit
);
    
    