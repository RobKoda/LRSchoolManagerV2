using System.Drawing;
using Ardalis.SmartEnum;
using LRSchoolV2.Domain.Persons;

// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Domain.CustomerPayments;

public class CustomerPaymentType : SmartEnum<CustomerPaymentType>
{
    public static readonly CustomerPaymentType BankCheck = new(nameof(BankCheck), 1, "Chèque", Color.BlueViolet, PersonSummaryLineType.BankCheck);
    public static readonly CustomerPaymentType BankTransfer = new(nameof(BankTransfer), 2, "Virement", Color.Aqua, PersonSummaryLineType.BankTransfer);
    public static readonly CustomerPaymentType Cash = new(nameof(Cash), 3, "Espèces", Color.Lime, PersonSummaryLineType.Cash);

    public string DisplayName { get; }
    private Color Color { get; }

    public string RgbaString => $"rgba({Color.R}, {Color.G}, {Color.B}, 0.3)";
    public PersonSummaryLineType PersonSummaryLineType { get; }
    
    private CustomerPaymentType(string inName, int inValue, string inDisplayName, Color inColor, PersonSummaryLineType inPersonSummaryLineType) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
        Color = inColor;
        PersonSummaryLineType = inPersonSummaryLineType;
    }
}