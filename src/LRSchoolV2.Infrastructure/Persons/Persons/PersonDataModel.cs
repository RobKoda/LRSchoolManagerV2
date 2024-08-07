using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.Common.Addresses;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;
using LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments;
using LRSchoolV2.Infrastructure.Persons.PersonRegistrations;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
namespace LRSchoolV2.Infrastructure.Persons.Persons;

[Table(nameof(Person))]
public class PersonDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(128)]
    public string LastName { get; set; } = string.Empty;
    
    [StringLength(128)]
    public string FirstName { get; set; } = string.Empty;
    
    public DateTime? BirthDate { get; set; }

    [StringLength(32)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(512)]
    public string Email { get; set; } = string.Empty;
    
    [ForeignKey(nameof(Address))]
    public Guid AddressId { get; set; }
    public AddressDataModel? Address { get; set; }
    
    [ForeignKey(nameof(ContactPerson1))]
    public Guid? ContactPerson1Id { get; set; }
    public PersonDataModel? ContactPerson1 { get; set; }
    
    [ForeignKey(nameof(ContactPerson2))]
    public Guid? ContactPerson2Id { get; set; }
    public PersonDataModel? ContactPerson2 { get; set; }
    
    public bool BillingToContactPerson1 { get; set; }

    [InverseProperty(nameof(PersonRegistrationDataModel.Person))]
    public ICollection<PersonRegistrationDataModel> Registrations { get; set; } = new List<PersonRegistrationDataModel>();
    
    [InverseProperty(nameof(CustomerInvoiceDataModel.Customer))]
    public ICollection<CustomerInvoiceDataModel> CustomerInvoices { get; set; } = new List<CustomerInvoiceDataModel>();
    
    [InverseProperty(nameof(CustomerPaymentDataModel.Person))]
    public ICollection<CustomerPaymentDataModel> CustomerPayments { get; set; } = new List<CustomerPaymentDataModel>();
}