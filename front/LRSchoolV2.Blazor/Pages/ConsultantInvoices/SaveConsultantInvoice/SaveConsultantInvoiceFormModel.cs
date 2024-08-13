using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices.SaveConsultantInvoice;

public class SaveConsultantInvoiceFormModel
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
    
    public Consultant Consultant { get; set; }
    
    public IList<SaveConsultantInvoiceItemFormModel> Items { get; set; } = [];
    
    public IEnumerable<ConsultantInvoice> ConsultantInvoices { get; set; }

    public SaveConsultantInvoiceFormModel(IEnumerable<ConsultantInvoice> inConsultantInvoices, Consultant inConsultant)
    {
        ConsultantInvoices = inConsultantInvoices;
        Consultant = inConsultant;
        Date = DateTime.Today;
    }
    
    private string GetNumber()
    {
        return Date.HasValue ? ConsultantInvoice.GetInvoiceNumber(Date.Value, ConsultantInvoices, Consultant.Id) : string.Empty;
    }
    
    public int GetNextOrder() => Items.Count > 0 ? Items.Max(inItem => inItem.Order) + 1 : 1;
}