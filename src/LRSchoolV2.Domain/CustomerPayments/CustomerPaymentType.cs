using System.Drawing;
using Ardalis.SmartEnum;

// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Domain.CustomerPayments;

public class CustomerPaymentType : SmartEnum<CustomerPaymentType>
{
    public static readonly CustomerPaymentType BankCheck = new(nameof(BankCheck), 1, "Chèque", Color.BlueViolet);
    public static readonly CustomerPaymentType BankTransfer = new(nameof(BankTransfer), 2, "Virement", Color.Aqua);
    public static readonly CustomerPaymentType Cash = new(nameof(Cash), 3, "Espèces", Color.Lime);

    public string DisplayName { get; }
    private Color Color { get; }

    public string RgbaString => $"rgba({Color.R}, {Color.G}, {Color.B}, 0.3)";
    
    private CustomerPaymentType(string inName, int inValue, string inDisplayName, Color inColor) : base(inName, inValue)
    {
        DisplayName = inDisplayName;
        Color = inColor;
    }
}