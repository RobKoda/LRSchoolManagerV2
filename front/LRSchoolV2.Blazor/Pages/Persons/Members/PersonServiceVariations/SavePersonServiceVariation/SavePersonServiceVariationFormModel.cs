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
    public AnnualServiceVariation? ServiceVariation { get; set; }
    
    public int PaymentsCount { get; set; }
    
    public Person? BilledPerson { get; set; }
}