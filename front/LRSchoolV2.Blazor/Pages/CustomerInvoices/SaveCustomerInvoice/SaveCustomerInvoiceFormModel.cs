using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.SaveCustomerInvoice;

public class SaveCustomerInvoiceFormModel
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
    
    public IList<SaveCustomerInvoiceItemFormModel> Items { get; set; } = [];
    
    public IEnumerable<CustomerInvoice> CustomerInvoices { get; set; }
    
    public SaveCustomerInvoiceFormModel(IEnumerable<CustomerInvoice> inCustomerInvoices)
    {
        CustomerInvoices = inCustomerInvoices;
        Date = DateTime.Today;
    }
    
    private string GetNumber()
    {
        return Date.HasValue ? CustomerInvoice.GetInvoiceNumber(Date.Value, CustomerInvoices) : string.Empty;
    }

    public int GetNextOrder() => Items.Count > 0 ? Items.Max(inItem => inItem.Order) + 1 : 1;
}