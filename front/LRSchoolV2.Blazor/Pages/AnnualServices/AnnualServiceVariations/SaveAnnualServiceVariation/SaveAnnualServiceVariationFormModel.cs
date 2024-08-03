using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.AnnualServices;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.SaveAnnualServiceVariation;

public class SaveAnnualServiceVariationFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public AnnualService AnnualService { get; set; } = null!;
    
    [Required(ErrorMessage = "Le nom est requis")]
    [MaxLength(256, ErrorMessage = "Le nom est trop long")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom est requis")]
    [MaxLength(256, ErrorMessage = "Le nom est trop long")]
    public string InvoiceName { get; set; } = string.Empty;
}