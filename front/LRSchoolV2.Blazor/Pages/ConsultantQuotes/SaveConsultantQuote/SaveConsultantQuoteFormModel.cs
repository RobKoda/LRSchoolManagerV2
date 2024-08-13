using System.ComponentModel.DataAnnotations;
using LRSchoolV2.Domain.ConsultantQuotes;
using LRSchoolV2.Domain.Consultants;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.ConsultantQuotes.SaveConsultantQuote;

public class SaveConsultantQuoteFormModel
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
    
    private Consultant? _consultant;
    [Required(ErrorMessage = "L'intervenant est requis")]
    public Consultant? Consultant
    {
        get => _consultant;
        set
        {
            _consultant = value;
            Number = GetNumber();
        }
    }
    
    public IList<SaveConsultantQuoteItemFormModel> Items { get; set; } = [];
    
    public IEnumerable<ConsultantQuote> ConsultantQuotes { get; set; }

    public SaveConsultantQuoteFormModel(IEnumerable<ConsultantQuote> inConsultantQuotes)
    {
        ConsultantQuotes = inConsultantQuotes;
        Date = DateTime.Today;
    }
    
    private string GetNumber()
    {
        return Date.HasValue && Consultant != null ? ConsultantQuote.GetQuoteNumber(Date.Value, ConsultantQuotes, Consultant.Id) : string.Empty;
    }
    
    public int GetNextOrder() => Items.Count > 0 ? Items.Max(inItem => inItem.Order) + 1 : 1;
}