using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.CustomerPayments;
using LRSchoolV2.Infrastructure.Persons.Persons;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments;

[Table(nameof(CustomerPayment))]
public class CustomerPaymentDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Person))]
    public Guid PersonId { get; set; }
    public PersonDataModel? Person { get; set; }

    public DateTime Date { get; set; }
    
    public int CustomerPaymentTypeValue { get; set; }
    
    public decimal Amount { get; set; }

    [MaxLength(64)]
    public string Reference { get; set; } = string.Empty;
    
    [MaxLength(1024)]
    public string Comment { get; set; } = string.Empty;
}