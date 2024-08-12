using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.ConsultantInvoices;

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;

public interface IConsultantInvoiceItemsRepository : IRepository
{
    Task<IEnumerable<ConsultantInvoiceItem>> GetConsultantInvoiceItemsPerConsultantInvoiceAsync(Guid inConsultantInvoiceId);
    Task SaveConsultantInvoiceItemAsync(ConsultantInvoiceItem inConsultantInvoiceItem);
    Task DeleteConsultantInvoiceItemAsync(Guid inConsultantInvoiceItemId);
}