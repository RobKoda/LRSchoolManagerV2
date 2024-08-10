using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.CustomerQuotes;
using LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems;
using LRSchoolV2.Infrastructure.Persons.Persons;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;

[Table(nameof(CustomerQuote))]
public class CustomerQuoteDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(32)]
    public string Number { get; set; } = string.Empty;
    
    public DateTime Date { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public PersonDataModel? Customer { get; set; }

    [MaxLength(512)]
    public string QuoteCustomerName { get; set; } = string.Empty;
    
    [MaxLength(2048)]
    public string QuoteCustomerAddress { get; set; } = string.Empty;
    
    public bool EmailSent { get; set; }
    
    public ICollection<CustomerQuoteItemDataModel> Items { get; set; } = new List<CustomerQuoteItemDataModel>();
}