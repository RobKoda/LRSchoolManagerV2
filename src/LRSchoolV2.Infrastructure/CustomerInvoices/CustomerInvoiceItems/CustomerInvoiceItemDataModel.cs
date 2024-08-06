using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;

[Table(nameof(CustomerInvoiceItem))]
public class CustomerInvoiceItemDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(CustomerInvoice))]
    public Guid CustomerInvoiceId { get; set; }
    public CustomerInvoiceDataModel? CustomerInvoice { get; set; }
    
    public Guid ReferenceId { get; set; }
    
    public int Quantity { get; set; }

    [MaxLength(512)]
    public string Denomination { get; set; } = string.Empty;
    
    public decimal UnitPrice { get; set; }
}