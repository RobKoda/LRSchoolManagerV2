using Ardalis.SmartEnum;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Chill bro, it's fine
namespace LRSchoolV2.Domain.Persons;

public class PersonSummaryLineType : SmartEnum<PersonSummaryLineType>
{
    public static readonly PersonSummaryLineType CustomerInvoice = new (nameof(CustomerInvoice), 1, "Facture");
    public static readonly PersonSummaryLineType BankTransfer = new (nameof(CustomerInvoice), 2, "Virement");
    public static readonly PersonSummaryLineType BankCheck = new (nameof(BankCheck), 3, "Chèque");
    public static readonly PersonSummaryLineType Cash = new (nameof(Cash), 4, "Liquide");

    public string DisplayName { get; }
    
    private PersonSummaryLineType(string inName, int inValue, string inDisplayName) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
    }
}