using Ardalis.SmartEnum;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Chill bro, it's fine

namespace LRSchoolV2.Domain.CustomerInvoices;

public class PayableReferenceType : SmartEnum<PayableReferenceType>
{
    public static readonly PayableReferenceType PersonRegistration = new (nameof(PersonRegistration), 1, "Adhésion");
    public static readonly PayableReferenceType PersonServiceVariation = new (nameof(PersonServiceVariation), 2, "Service");

    public string DisplayName { get; }
    
    private PayableReferenceType(string inName, int inValue, string inDisplayName) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
    }
}