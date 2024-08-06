using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;
using LRSchoolV2.Infrastructure.Persons.Persons;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;

[Table(nameof(CustomerInvoice))]
public class CustomerInvoiceDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(32)]
    public string Number { get; set; } = string.Empty;
    
    public DateTime Date { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public PersonDataModel? Customer { get; set; }

    [MaxLength(512)]
    public string InvoiceCustomerName { get; set; } = string.Empty;
    
    [MaxLength(2048)]
    public string InvoiceCustomerAddress { get; set; } = string.Empty;
    
    public bool EmailSent { get; set; }
    
    public ICollection<CustomerInvoiceItemDataModel> Items { get; set; } = new List<CustomerInvoiceItemDataModel>();
}