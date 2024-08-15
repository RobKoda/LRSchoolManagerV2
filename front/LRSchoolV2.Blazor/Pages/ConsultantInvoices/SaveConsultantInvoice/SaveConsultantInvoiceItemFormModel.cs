using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices.SaveConsultantInvoice;

public class SaveConsultantInvoiceItemFormModel(int inOrder)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public ConsultantInvoice? ConsultantInvoice { get; set; }
    
    public SchoolYear? SchoolYear { get; set; }
    
    public string Denomination { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public int Order { get; set; } = inOrder;
    
    public decimal GetTotal() => Quantity * UnitPrice;
}