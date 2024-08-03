using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Blazor.Shared.Validators;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using Mapster;

// ReSharper disable MemberCanBePrivate.Global - Implicit use
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Persons.Members.SaveMember;

public class SaveMemberFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Le nom est requis")]
    [MaxLength(128, ErrorMessage = "Le nom est trop long")]
    public string LastName { get; set; } = string.Empty;
    
    [MaxLength(128, ErrorMessage = "Le prénom est trop long")]
    public string FirstName { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }

    [MaxLength(32, ErrorMessage = "Le téléphone est trop long")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [MaxLength(512, ErrorMessage = "L'email est trop long")]
    [EmailAddressWithEmpty(ErrorMessage = "L'adresse email est invalide")]
    public string Email { get; set; } = string.Empty;

    public Guid AddressId { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "L'adresse est requise")]
    [MaxLength(1024, ErrorMessage = "L'adresse est trop longue")]
    public string AddressStreet { get; set; } = string.Empty;
    
    [MaxLength(1024, ErrorMessage = "Le complément d'adresse est trop longue")]
    public string AddressStreetComplement { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le code postal est requis")]
    [MaxLength(16, ErrorMessage = "Le code postal est trop long")]
    public string AddressZipCode { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La ville est requise")]
    [MaxLength(256, ErrorMessage = "La ville est trop longue")]
    public string AddressCity { get; set; } = string.Empty;
    
    public Person? ContactPerson1 { get; set; }
    
    public Person? ContactPerson2 { get; set; }

    public bool BillingToContactPerson1 { get; set; }
    
    public bool ImageRightsGranted { get; set; }
    public SchoolYear? SchoolYear { get; set; }

    public SaveMemberFormModel()
    {
    }
    
    public SaveMemberFormModel(SchoolYear inSchoolYear) : this()
    {
        SchoolYear = inSchoolYear;
    }

    public PersonRegistration GetInitialRegistration() => new(Guid.NewGuid(), this.Adapt<Person>(), SchoolYear!, ImageRightsGranted, null);
}