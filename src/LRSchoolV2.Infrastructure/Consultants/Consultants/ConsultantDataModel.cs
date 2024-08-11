using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.Consultants;
using LRSchoolV2.Infrastructure.Common.Addresses;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Consultants.Consultants;

[Table(nameof(Consultant))]
public class ConsultantDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(128)]
    public string LastName { get; set; } = string.Empty;
    
    [StringLength(128)]
    public string FirstName { get; set; } = string.Empty;
    
    [StringLength(128)]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(32)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(512)]
    public string Email { get; set; } = string.Empty;
    
    [ForeignKey(nameof(Address))]
    public Guid AddressId { get; set; }
    public AddressDataModel? Address { get; set; }
    
    [StringLength(64)]
    public string Iban { get; set; } = string.Empty;
    
    [StringLength(64)]
    public string BicCode { get; set; } = string.Empty;
    
    public byte[] QuoteDocument { get; set; } = [];
    
    public byte[] InvoiceDocument { get; set; } = [];
}