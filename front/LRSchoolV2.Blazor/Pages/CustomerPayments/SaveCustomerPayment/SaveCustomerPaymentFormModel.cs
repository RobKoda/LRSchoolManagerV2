using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.CustomerPayments;
using LRSchoolV2.Domain.Persons;

// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CustomerPayments.SaveCustomerPayment;

public class SaveCustomerPaymentFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "La personne est requise")]
    public Person? Person { get; set; }
    
    [Required(ErrorMessage = "Le type de paiement est requis")]
    public CustomerPaymentType? CustomerInvoicePaymentType { get; set; }
    
    [Required(ErrorMessage = "La date est requise")]
    public DateTime? Date { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Le montant doit être positif")]
    public decimal Amount { get; set; }
    
    [MaxLength(64, ErrorMessage = "La référence est trop long")]
    public string Reference { get; set; } = string.Empty;
    
    [MaxLength(1024, ErrorMessage = "Le commentaire est trop long")]
    public string Comment { get; set; } = string.Empty;
}