using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.CustomerQuotes;
using LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems;

[Table(nameof(CustomerQuoteItem))]
public class CustomerQuoteItemDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(CustomerQuote))]
    public Guid CustomerQuoteId { get; set; }
    public CustomerQuoteDataModel? CustomerQuote { get; set; }
    
    public int Quantity { get; set; }

    [MaxLength(512)]
    public string Denomination { get; set; } = string.Empty;
    
    public decimal UnitPrice { get; set; }
}