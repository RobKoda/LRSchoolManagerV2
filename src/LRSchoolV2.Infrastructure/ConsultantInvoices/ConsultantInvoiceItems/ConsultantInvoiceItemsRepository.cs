using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;
using LRSchoolV2.Domain.ConsultantInvoices;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoiceItems;

public class ConsultantInvoiceItemsRepository(IDbContextFactory<ApplicationContext> inContext) : IConsultantInvoiceItemsRepository
{
    public Task<IEnumerable<ConsultantInvoiceItem>> GetConsultantInvoiceItemsAsync() =>
        inContext.GetAllAsync<ConsultantInvoiceItemDataModel, ConsultantInvoiceItem>();
    
    public Task<IEnumerable<ConsultantInvoiceItem>> GetConsultantInvoiceItemsPerConsultantInvoiceAsync(Guid inConsultantInvoiceId) =>
        inContext.GetAllAsync<ConsultantInvoiceItemDataModel, ConsultantInvoiceItem>(inQueryable => GetConsultantInvoiceItemQueryableAsync(inQueryable)
            .Where(inInvoiceItem => inInvoiceItem.ConsultantInvoiceId == inConsultantInvoiceId)
        );

    public Task DeleteConsultantInvoiceItemAsync(Guid inConsultantInvoiceItemId) =>
        inContext.DeleteAsync<ConsultantInvoiceItemDataModel>(inConsultantInvoiceItemId);

    public Task SaveConsultantInvoiceItemAsync(ConsultantInvoiceItem inPerson) =>
        inContext.SaveAsync<ConsultantInvoiceItemDataModel, ConsultantInvoiceItem>(inPerson);

    private static IQueryable<ConsultantInvoiceItemDataModel> GetConsultantInvoiceItemQueryableAsync(IQueryable<ConsultantInvoiceItemDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.ConsultantInvoice);
}