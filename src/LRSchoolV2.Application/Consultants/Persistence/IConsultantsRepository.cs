using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.Consultants;

namespace LRSchoolV2.Application.Consultants.Persistence;

public interface IConsultantsRepository : IRepository
{
    Task<IEnumerable<Consultant>> GetConsultantsAsync();
    Task SaveConsultantAsync(Consultant inConsultant);
    Task DeleteConsultantAsync(Guid inRequestConsultantId);
    Task<bool> AnyConsultantAsync(Guid inContactPersonId);
    Task<Option<Consultant>> GetConsultantAsync(Guid inConsultantId);
    Task<bool> CanConsultantBeDeleted(Guid inConsultantId);
    Task SetConsultantQuoteDocument(Guid inConsultantId, byte[] inConsultantQuoteDocument);
    Task SetConsultantInvoiceDocument(Guid inConsultantId, byte[] inConsultantInvoiceDocument);
    Task<Option<byte[]>> GetConsultantQuoteDocument(Guid inConsultantId);
    Task<Option<byte[]>> GetConsultantInvoiceDocument(Guid inConsultantId);
}