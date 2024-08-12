using LRSchoolV2.Domain.CustomerQuotes;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.CustomerQuotes.SaveCustomerQuote;

public class SaveCustomerQuoteItemFormModel(int inOrder)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public CustomerQuote? CustomerQuote { get; set; }
    
    public string Denomination { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public int Order { get; set; } = inOrder;
    
    public decimal GetTotal() => Quantity * UnitPrice;
}