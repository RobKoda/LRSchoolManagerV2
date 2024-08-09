using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;

// ReSharper disable UnusedMember.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.SaveCustomerInvoice;

public class SaveCustomerInvoiceFormModel(IEnumerable<CustomerInvoice> inCustomerInvoices)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Number => Date.HasValue ? CustomerInvoice.GetInvoiceNumber(Date.Value, CustomerInvoices) : string.Empty;
    
    [Required(ErrorMessage = "La date est requise")]
    public DateTime? Date { get; set; } = DateTime.Today;
    
    [Required(ErrorMessage = "La personne est requise")]
    public Person? Customer { get; set; }
    
    public IList<SaveCustomerInvoiceItemFormModel> Items { get; set; } = [];
    
    public IEnumerable<CustomerInvoice> CustomerInvoices { get; set; } = inCustomerInvoices;
}