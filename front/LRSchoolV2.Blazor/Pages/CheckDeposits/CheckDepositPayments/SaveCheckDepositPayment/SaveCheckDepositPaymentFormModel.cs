using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.CustomerPayments;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;

public class SaveCheckDepositPaymentFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid CheckDepositId { get; set; }
    
    [Required(ErrorMessage = "Le chèque est requis")]
    public CustomerPayment? CustomerPayment { get; set; }
}