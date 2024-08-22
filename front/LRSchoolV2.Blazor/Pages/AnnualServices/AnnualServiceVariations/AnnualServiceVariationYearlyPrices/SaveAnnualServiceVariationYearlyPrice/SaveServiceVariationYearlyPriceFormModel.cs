using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.AnnualServiceVariationYearlyPrices.SaveAnnualServiceVariationYearlyPrice;

public class SaveServiceVariationYearlyPriceFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public AnnualServiceVariation AnnualServiceVariation { get; set; } = null!;
    
    [Required(ErrorMessage = "L'année scolaire est requise")]
    public SchoolYear? SchoolYear { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Le prix doit être positif")]
    public decimal Price { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Le prix doit être positif")]
    public decimal Margin { get; set; }
}