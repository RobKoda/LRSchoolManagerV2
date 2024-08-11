using LanguageExt;
using LRSchoolV2.Application.Consultants.Persistence;
using LRSchoolV2.Domain.Consultants;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Infrastructure.Consultants.Consultants;

public class ConsultantsRepository(IDbContextFactory<ApplicationContext> inContext) : IConsultantsRepository
{
    public Task<IEnumerable<Consultant>> GetConsultantsAsync() =>
        inContext.GetAllAsync<ConsultantDataModel, Consultant>(GetConsultantQueryableAsync);

    public Task<Option<Consultant>> GetConsultantAsync(Guid inId) =>
        inContext.GetSingleAsync<ConsultantDataModel, Consultant>(inId, GetConsultantQueryableAsync);

    public Task<bool> AnyConsultantAsync(Guid inId) =>
        inContext.AnyAsync<ConsultantDataModel>(inId);

    public Task DeleteConsultantAsync(Guid inConsultantId) =>
        inContext.DeleteAsync<ConsultantDataModel>(inConsultantId);

    public Task SaveConsultantAsync(Consultant inConsultant) =>
        inContext.SaveAsync<ConsultantDataModel, Consultant>(inConsultant);

    public Task<bool> CanConsultantBeDeleted(Guid inConsultantId) =>
        inContext.CanBeDeleted<ConsultantDataModel>(inConsultantId);
    
    public async Task SetConsultantQuoteDocument(Guid inConsultantId, byte[] inConsultantQuoteDocument) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<ConsultantDataModel>())
            .Where(inConsultant => inConsultant.Id == inConsultantId)
            .ExecuteUpdateAsync(inUpdate => inUpdate.SetProperty(
                inConsultant => inConsultant.QuoteDocument, inConsultantQuoteDocument));
    
    public async Task<Option<byte[]>> GetConsultantQuoteDocument(Guid inConsultantId) =>
        (await inContext.GetSingleAsync<ConsultantDataModel>(inConsultantId))
        .Map(inConsultant => inConsultant.QuoteDocument);
    
    public async Task<Option<byte[]>> GetConsultantInvoiceDocument(Guid inConsultantId) =>
        (await inContext.GetSingleAsync<ConsultantDataModel>(inConsultantId))
        .Map(inConsultant => inConsultant.InvoiceDocument);
    
    public async Task SetConsultantInvoiceDocument(Guid inConsultantId, byte[] inConsultantInvoiceDocument) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<ConsultantDataModel>())
            .Where(inConsultant => inConsultant.Id == inConsultantId)
            .ExecuteUpdateAsync(inUpdate => inUpdate.SetProperty(
                inConsultant => inConsultant.InvoiceDocument, inConsultantInvoiceDocument));

    private static IQueryable<ConsultantDataModel> GetConsultantQueryableAsync(IQueryable<ConsultantDataModel> inQueryable) =>
        inQueryable
            .Include(inConsultant => inConsultant.Address);
}