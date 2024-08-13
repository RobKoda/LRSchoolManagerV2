using LanguageExt;
using static LanguageExt.Prelude;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;
using LRSchoolV2.Domain.ConsultantQuotes;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuotes;

public class ConsultantQuotesRepository(IDbContextFactory<ApplicationContext> inContext) : IConsultantQuotesRepository
{
    public Task<Option<ConsultantQuote>> GetConsultantQuoteAsync(Guid inConsultantQuoteId) =>
        inContext.GetSingleAsync<ConsultantQuoteDataModel, ConsultantQuote>(inConsultantQuoteId, GetConsultantQuoteQueryableAsync);
    
    public async Task<Option<ConsultantQuote>> GetLastConsultantQuoteAsync(Guid inConsultantId) =>
        Optional(
            (await (await inContext.GetQueryableAsNoTrackingAsync<ConsultantQuoteDataModel>())
                .Where(inQuote => inQuote.ConsultantId == inConsultantId)
                .OrderByDescending(inQuote => inQuote.Date)
                .FirstOrDefaultAsync())
            .Adapt<ConsultantQuote>());
    
    public Task<IEnumerable<ConsultantQuote>> GetConsultantQuotesAsync() =>
        inContext.GetAllAsync<ConsultantQuoteDataModel, ConsultantQuote>(GetConsultantQuoteQueryableAsync);

    public Task<bool> AnyConsultantQuoteAsync(Guid inId) =>
        inContext.AnyAsync<ConsultantQuoteDataModel>(inId);

    public async Task<bool> CanConsultantQuoteBeDeletedAsync(Guid inId) =>
        await (await inContext.GetSingleAsync<ConsultantQuoteDataModel>(inId))
            .MatchAsync(
                async inToValidate =>
                {
                    var context = await inContext.GetContextAsync();
                    await context.Database.BeginTransactionAsync();
                    
                    var document = await context.Documents.SingleOrDefaultAsync(inDocument => inDocument.ReferenceId == inToValidate.Id);
                    if (document != null)
                    {
                        context.Remove(document);
                    }
                    
                    var items = await context.ConsultantQuoteItems
                        .Where(inItem => inItem.ConsultantQuoteId == inToValidate.Id)
                        .ToListAsync();
                    context.RemoveRange(items);
                    
                    context.Remove(inToValidate);
                    try
                    {
                        await context.SaveChangesAsync();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        await context.Database.RollbackTransactionAsync();
                    }
                },
                () => false);
    
    public Task DeleteConsultantQuoteAsync(Guid inPersonId) =>
        inContext.DeleteAsync<ConsultantQuoteDataModel>(inPersonId);
    
    public Task SaveConsultantQuoteAsync(ConsultantQuote inPerson) =>
        inContext.SaveAsync<ConsultantQuoteDataModel, ConsultantQuote>(inPerson);
    
    private static IQueryable<ConsultantQuoteDataModel> GetConsultantQuoteQueryableAsync(IQueryable<ConsultantQuoteDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Consultant)
            .Include(inPerson => inPerson.Items);
}