using Ardalis.SmartEnum;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Chill bro, it's fine

namespace LRSchoolV2.Domain.ConsultantInvoices;

public class ConsultantInvoiceableReferenceType : SmartEnum<ConsultantInvoiceableReferenceType>
{
    public static readonly ConsultantInvoiceableReferenceType GlobalAnnualServiceWork = new (nameof(GlobalAnnualServiceWork), 1, "Travail global service annuel");
    public static readonly ConsultantInvoiceableReferenceType PerStudentAnnualServiceVariationWork = new (nameof(PerStudentAnnualServiceVariationWork), 2, "Travail par élève variation de service annuel");

    public string DisplayName { get; }
    
    private ConsultantInvoiceableReferenceType(string inName, int inValue, string inDisplayName) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
    }
}