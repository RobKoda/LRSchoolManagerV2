using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.Persistence;
using LRSchoolV2.Domain.ConsultantQuotes;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuoteItems;

public class ConsultantQuoteItemsRepository(IDbContextFactory<ApplicationContext> inContext) : IConsultantQuoteItemsRepository
{
    public Task<IEnumerable<ConsultantQuoteItem>> GetConsultantQuoteItemsPerConsultantQuoteAsync(Guid inConsultantQuoteId) =>
        inContext.GetAllAsync<ConsultantQuoteItemDataModel, ConsultantQuoteItem>(inQueryable => GetConsultantQuoteItemQueryableAsync(inQueryable)
            .Where(inQuoteItem => inQuoteItem.ConsultantQuoteId == inConsultantQuoteId)
        );

    public Task DeleteConsultantQuoteItemAsync(Guid inConsultantQuoteItemId) =>
        inContext.DeleteAsync<ConsultantQuoteItemDataModel>(inConsultantQuoteItemId);

    public Task SaveConsultantQuoteItemAsync(ConsultantQuoteItem inPerson) =>
        inContext.SaveAsync<ConsultantQuoteItemDataModel, ConsultantQuoteItem>(inPerson);

    private static IQueryable<ConsultantQuoteItemDataModel> GetConsultantQuoteItemQueryableAsync(IQueryable<ConsultantQuoteItemDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.ConsultantQuote);
}