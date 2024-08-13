using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.ConsultantInvoices;

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;

public interface IConsultantInvoicesRepository : IRepository
{
    Task<Option<ConsultantInvoice>> GetConsultantInvoiceAsync(Guid inConsultantInvoiceId);
    Task<Option<ConsultantInvoice>> GetLastConsultantInvoiceAsync(Guid inConsultantId);
    Task<IEnumerable<ConsultantInvoice>> GetConsultantInvoicesAsync();
    Task SaveConsultantInvoiceAsync(ConsultantInvoice inConsultantInvoice);
    Task<bool> AnyConsultantInvoiceAsync(Guid inContactConsultantInvoiceId);
    Task<bool> CanConsultantInvoiceBeDeletedAsync(Guid inContactConsultantInvoiceId);
    Task DeleteConsultantInvoiceAsync(Guid inConsultantInvoiceId);
}