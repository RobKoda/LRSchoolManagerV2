using Ardalis.SmartEnum;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Chill bro, it's fine

namespace LRSchoolV2.Domain.CustomerInvoices;

public class CustomerInvoiceableReferenceType : SmartEnum<CustomerInvoiceableReferenceType>
{
    public static readonly CustomerInvoiceableReferenceType PersonRegistration = new (nameof(PersonRegistration), 1, "Adhésion");
    public static readonly CustomerInvoiceableReferenceType PersonAnnualServiceVariation = new (nameof(PersonAnnualServiceVariation), 2, "Service annuel");

    public string DisplayName { get; }
    
    private CustomerInvoiceableReferenceType(string inName, int inValue, string inDisplayName) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
    }
}