using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Persons.Members.PersonServiceVariations.SavePersonServiceVariation;

public class SavePersonServiceVariationFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Le membre est requis")]
    public Person? Person { get; set; }
    
    [Required(ErrorMessage = "L'année scolaire est requise")]
    public SchoolYear? SchoolYear { get; set; }
    
    [Required(ErrorMessage = "Le service est requis")]
    public AnnualServiceVariation? AnnualServiceVariation { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Le nombre de paiement doit être positif")]
    public int PaymentsCount { get; set; } = 10;
    
    public Person? BilledPerson { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Le nombre de paiement de l'intervenant doit être positif")]
    public int ConsultantPaymentsCount { get; set; } = 12;
}