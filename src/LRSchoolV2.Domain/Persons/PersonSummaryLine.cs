namespace LRSchoolV2.Domain.Persons;

public record PersonSummaryLine(
    DateTime Date,
    PersonSummaryLineType Type,
    string Reference,
    decimal Debit,
    decimal Credit
)
{
    public PersonSummaryLineDisplay GetDisplay() => new(
        Date.ToString("yyyy-MM-dd"), 
        Type.DisplayName, 
        Reference, 
        Debit == 0 ? string.Empty : Debit.ToString("0.00"), 
        Credit == 0 ? string.Empty : Credit.ToString("0.00"));
}