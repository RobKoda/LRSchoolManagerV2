using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CheckDeposits.SaveCheckDeposit;

public class SaveCheckDepositFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "La date est obligatoire")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "Le numéro de remise de chèque est requis")]
    [MaxLength(64, ErrorMessage = "Le numéro de remise de chèque est trop long")]
    public string Number { get; set; } = string.Empty;
}