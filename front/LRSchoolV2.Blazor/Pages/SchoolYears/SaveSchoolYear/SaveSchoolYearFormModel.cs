using LRSchoolV2.Blazor.Shared.Validators;
using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "La date de début est obligatoire")]
    public DateTime? StartDate { get; set; }
    
    [Required(ErrorMessage = "La date de fin est obligatoire")]
    [DateGreaterThan(nameof(StartDate), ErrorMessage = "La date de fin doit être supérieure à la date de début")]
    public DateTime? EndDate { get; set; }
    
    [Required(ErrorMessage = "La cotisation est requise")]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "La cotisation doit être positive")]
    public decimal MembershipFee { get; set; }
}