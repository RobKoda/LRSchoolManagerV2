using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.Common;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Common.Addresses;

[Table(nameof(Address))]
public class AddressDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [StringLength(1024)]
    public string Street { get; set; } = string.Empty;
    
    [StringLength(1024)]
    public string StreetComplement { get; set; } = string.Empty;
    
    [StringLength(16)]
    public string ZipCode { get; set; } = string.Empty;

    [StringLength(256)]
    public string City { get; set; } = string.Empty;
}