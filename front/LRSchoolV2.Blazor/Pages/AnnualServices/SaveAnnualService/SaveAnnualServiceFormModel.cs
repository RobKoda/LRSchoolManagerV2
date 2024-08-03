using System.ComponentModel.DataAnnotations;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.SaveAnnualService;

public class SaveAnnualServiceFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Le nom est requis")]
    [MaxLength(256, ErrorMessage = "Le nom est trop long")]
    public string Name { get; set; } = string.Empty;
}