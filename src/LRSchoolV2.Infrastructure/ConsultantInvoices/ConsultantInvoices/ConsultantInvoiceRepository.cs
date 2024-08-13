using LanguageExt;
using static LanguageExt.Prelude;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;
using LRSchoolV2.Domain.ConsultantInvoices;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoices;

public class ConsultantInvoicesRepository(IDbContextFactory<ApplicationContext> inContext) : IConsultantInvoicesRepository
{
    public Task<Option<ConsultantInvoice>> GetConsultantInvoiceAsync(Guid inConsultantInvoiceId) =>
        inContext.GetSingleAsync<ConsultantInvoiceDataModel, ConsultantInvoice>(inConsultantInvoiceId, GetConsultantInvoiceQueryableAsync);
    
    public async Task<Option<ConsultantInvoice>> GetLastConsultantInvoiceAsync(Guid inConsultantId) =>
        Optional(
            (await (await inContext.GetQueryableAsNoTrackingAsync<ConsultantInvoiceDataModel>())
                .Where(inConsultantInvoiceDataModel => inConsultantInvoiceDataModel.ConsultantId == inConsultantId)
                .OrderByDescending(inInvoice => inInvoice.Date)
                .FirstOrDefaultAsync())
            .Adapt<ConsultantInvoice>());
    
    public Task<IEnumerable<ConsultantInvoice>> GetConsultantInvoicesAsync() =>
        inContext.GetAllAsync<ConsultantInvoiceDataModel, ConsultantInvoice>(GetConsultantInvoiceQueryableAsync);

    public Task<bool> AnyConsultantInvoiceAsync(Guid inId) =>
        inContext.AnyAsync<ConsultantInvoiceDataModel>(inId);

    public async Task<bool> CanConsultantInvoiceBeDeletedAsync(Guid inId) =>
        await (await inContext.GetSingleAsync<ConsultantInvoiceDataModel>(inId))
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
                    
                    var items = await context.ConsultantInvoiceItems
                        .Where(inItem => inItem.ConsultantInvoiceId == inToValidate.Id)
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
    
    public Task DeleteConsultantInvoiceAsync(Guid inPersonId) =>
        inContext.DeleteAsync<ConsultantInvoiceDataModel>(inPersonId);
    
    public Task SaveConsultantInvoiceAsync(ConsultantInvoice inPerson) =>
        inContext.SaveAsync<ConsultantInvoiceDataModel, ConsultantInvoice>(inPerson);
    
    private static IQueryable<ConsultantInvoiceDataModel> GetConsultantInvoiceQueryableAsync(IQueryable<ConsultantInvoiceDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Consultant)
            .Include(inPerson => inPerson.Items);
}