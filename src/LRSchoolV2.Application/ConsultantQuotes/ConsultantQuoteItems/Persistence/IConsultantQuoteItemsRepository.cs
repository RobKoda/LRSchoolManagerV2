using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.ConsultantQuotes;

namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.Persistence;

public interface IConsultantQuoteItemsRepository : IRepository
{
    Task<IEnumerable<ConsultantQuoteItem>> GetConsultantQuoteItemsPerConsultantQuoteAsync(Guid inConsultantQuoteId);
    Task SaveConsultantQuoteItemAsync(ConsultantQuoteItem inConsultantQuoteItem);
    Task DeleteConsultantQuoteItemAsync(Guid inConsultantQuoteItemId);
}