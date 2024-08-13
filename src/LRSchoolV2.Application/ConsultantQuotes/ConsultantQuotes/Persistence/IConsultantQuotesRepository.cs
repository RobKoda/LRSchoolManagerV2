using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.ConsultantQuotes;

namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;

public interface IConsultantQuotesRepository : IRepository
{
    Task<Option<ConsultantQuote>> GetConsultantQuoteAsync(Guid inConsultantQuoteId);
    Task<Option<ConsultantQuote>> GetLastConsultantQuoteAsync(Guid inConsultantId);
    Task<IEnumerable<ConsultantQuote>> GetConsultantQuotesAsync();
    Task SaveConsultantQuoteAsync(ConsultantQuote inConsultantQuote);
    Task<bool> AnyConsultantQuoteAsync(Guid inContactConsultantQuoteId);
    Task<bool> CanConsultantQuoteBeDeletedAsync(Guid inContactConsultantQuoteId);
    Task DeleteConsultantQuoteAsync(Guid inConsultantQuoteId);
}