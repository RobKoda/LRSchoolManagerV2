using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.ConsultantQuotes;
using LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuotes;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuoteItems;

[Table(nameof(ConsultantQuoteItem))]
public class ConsultantQuoteItemDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(ConsultantQuote))]
    public Guid ConsultantQuoteId { get; set; }
    public ConsultantQuoteDataModel? ConsultantQuote { get; set; }
    
    public int Quantity { get; set; }

    [MaxLength(512)]
    public string Denomination { get; set; } = string.Empty;
    
    public decimal UnitPrice { get; set; }
    
    public int Order { get; set; }
}