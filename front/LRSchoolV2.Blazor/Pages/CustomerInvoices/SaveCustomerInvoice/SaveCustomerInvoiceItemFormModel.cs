using LRSchoolV2.Domain.CustomerInvoices;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.SaveCustomerInvoice;

public class SaveCustomerInvoiceItemFormModel(int inOrder)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public CustomerInvoice? CustomerInvoice { get; set; }
    
    public string Denomination { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public int Order { get; set; } = inOrder;
    
    public decimal GetTotal() => Quantity * UnitPrice;
}