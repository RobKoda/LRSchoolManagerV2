using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.CustomerQuotes;
using LRSchoolV2.Domain.Persons;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.CustomerQuotes.SaveCustomerQuote;

public class SaveCustomerQuoteFormModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Number { get; set; } = string.Empty;
    
    private DateTime? _date;
    [Required(ErrorMessage = "La date est requise")]
    public DateTime? Date
    {
        get => _date;
        set
        {
            _date = value;
            Number = GetNumber();
        }
    }
    
    [Required(ErrorMessage = "La personne est requise")]
    public Person? Customer { get; set; }
    
    public IList<SaveCustomerQuoteItemFormModel> Items { get; set; } = [];
    
    public IEnumerable<CustomerQuote> CustomerQuotes { get; set; }

    public SaveCustomerQuoteFormModel(IEnumerable<CustomerQuote> inCustomerQuotes)
    {
        CustomerQuotes = inCustomerQuotes;
        Date = DateTime.Today;
    }
    
    private string GetNumber()
    {
        return Date.HasValue ? CustomerQuote.GetQuoteNumber(Date.Value, CustomerQuotes) : string.Empty;
    }
    
    public int GetNextOrder() => Items.Count > 0 ? Items.Max(inItem => inItem.Order) + 1 : 1;
}