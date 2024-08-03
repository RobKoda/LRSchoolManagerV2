using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceConsultantWorks.SaveAnnualServiceConsultantWork;

public class SaveAnnualServiceConsultantWorkFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AnnualServiceId { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "L'année scolaire est requise")]
    public SchoolYear? SchoolYear { get; set; }
    
    [Required(ErrorMessage = "L'intervenant est requis")]
    public Consultant? Consultant { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Le nombre d'heures doit être positif")]
    public decimal CommonWorkHours { get; set; }
    
    [MaxLength(1024, ErrorMessage = "Le commentaire est trop long")]
    public string CommonWorkHoursComment { get; set; } = string.Empty;
}