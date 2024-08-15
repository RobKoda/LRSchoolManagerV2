using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoices;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoiceItems;

[Table(nameof(ConsultantInvoiceItem))]
public class ConsultantInvoiceItemDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(ConsultantInvoice))]
    public Guid ConsultantInvoiceId { get; set; }
    public ConsultantInvoiceDataModel? ConsultantInvoice { get; set; }
    
    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }
    public SchoolYearDataModel? SchoolYear { get; set; }
    
    public Guid? ReferenceId { get; set; }
    
    public int Quantity { get; set; }

    [MaxLength(512)]
    public string Denomination { get; set; } = string.Empty;
    
    public decimal UnitPrice { get; set; }
    
    public int Order { get; set; }
}