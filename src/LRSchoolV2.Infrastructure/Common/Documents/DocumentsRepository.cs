using LanguageExt;
using LRSchoolV2.Application.Common.Documents.Persistence;
using LRSchoolV2.Domain.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Infrastructure.Common.Documents;

public class DocumentsRepository(IDbContextFactory<ApplicationContext> inContext) : IDocumentsRepository
{
    public async Task<Option<Document>> GetDocumentPerReferenceIdAsync(Guid inReferenceId)
    {
        var theDocument = await (await inContext.GetQueryableAsNoTrackingAsync<DocumentDataModel>())
            .SingleOrDefaultAsync(inDocument => inDocument.ReferenceId == inReferenceId);
        
        return theDocument == default
            ? Option<Document>.None
            : Option<Document>.Some(theDocument.Adapt<Document>());
    }

    public async Task SaveDocumentAsync(Document inDocument)
    {
        await (await inContext.GetQueryableAsNoTrackingAsync<DocumentDataModel>())
            .Where(inDatabaseDocument => inDatabaseDocument.ReferenceId == inDocument.ReferenceId)
            .ExecuteDeleteAsync();
        await inContext.SaveAsync<DocumentDataModel, Document>(inDocument);
    }

    public async Task DeleteDocumentPerReferenceIdAsync(Guid inReferenceId) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<DocumentDataModel>())
            .Where(inDatabaseDocument => inDatabaseDocument.ReferenceId == inReferenceId)
            .ExecuteDeleteAsync();
}