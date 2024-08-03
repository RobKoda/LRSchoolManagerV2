using System.ComponentModel.DataAnnotations;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Consultants.SaveConsultant;

public class SaveConsultantFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Le nom est requis")]
    [MaxLength(128, ErrorMessage = "Le nom est trop long")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le prénom est requis")]
    [MaxLength(128, ErrorMessage = "Le prénom est trop long")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom d'entreprise est requis")]
    [MaxLength(128, ErrorMessage = "Le nom d'entreprise est trop long")]
    public string CompanyName { get; set; } = string.Empty;
    
    [MaxLength(128, ErrorMessage = "Le téléphone est trop long")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [MaxLength(512, ErrorMessage = "L'email est trop long")]
    [EmailAddress(ErrorMessage = "L'adresse email est invalide")]
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
    
    [MaxLength(64, ErrorMessage = "L'IBAN est trop long")]
    public string Iban { get; set; } = string.Empty;
        
    [MaxLength(64, ErrorMessage = "Le code BIC est trop long")]
    public string BicCode { get; set; } = string.Empty;
    
}