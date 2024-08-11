using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.ConsultantQuotes;
using LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuoteItems;
using LRSchoolV2.Infrastructure.Consultants.Consultants;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuotes;

[Table(nameof(ConsultantQuote))]
public class ConsultantQuoteDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(32)]
    public string Number { get; set; } = string.Empty;
    
    public DateTime Date { get; set; }
    
    [ForeignKey(nameof(Consultant))]
    public Guid ConsultantId { get; set; }
    public ConsultantDataModel? Consultant { get; set; }

    [MaxLength(512)]
    public string QuoteConsultantName { get; set; } = string.Empty;
    
    [MaxLength(2048)]
    public string QuoteConsultantAddress { get; set; } = string.Empty;
    
    public bool EmailSent { get; set; }
    
    public ICollection<ConsultantQuoteItemDataModel> Items { get; set; } = new List<ConsultantQuoteItemDataModel>();
}