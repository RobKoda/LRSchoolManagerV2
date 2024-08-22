using Ardalis.SmartEnum;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Chill bro, it's fine
namespace LRSchoolV2.Domain.ConsultantInvoices;

public class ConsultantInvoiceableReferenceType : SmartEnum<ConsultantInvoiceableReferenceType>
{
    public static readonly ConsultantInvoiceableReferenceType AnnualService = new (nameof(AnnualService), 1, "Service annuel");

    public string DisplayName { get; }
    
    private ConsultantInvoiceableReferenceType(string inName, int inValue, string inDisplayName) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
    }
}