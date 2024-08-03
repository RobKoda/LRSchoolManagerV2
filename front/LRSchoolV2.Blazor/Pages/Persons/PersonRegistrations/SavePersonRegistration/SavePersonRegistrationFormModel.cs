using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Persons.PersonRegistrations.SavePersonRegistration;

public class SavePersonRegistrationFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Le membre est requis")]
    public Person? Person { get; set; }
    
    [Required(ErrorMessage = "L'année scolaire est requise")]
    public SchoolYear? SchoolYear { get; set; }
    
    public Person? ContactPerson1 { get; set; }
    
    public Person? ContactPerson2 { get; set; }
    
    public bool ImageRightsGranted { get; set; }
    
    public Person? BilledPerson { get; set; }
}