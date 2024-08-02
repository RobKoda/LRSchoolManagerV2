using System.Diagnostics.CodeAnalysis;
using LanguageExt;
using static LanguageExt.Prelude;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedMember.Global - Extension methods
// ReSharper disable MemberCanBePrivate.Global - Extension methods

namespace LRSchoolV2.Infrastructure.Core;

[ExcludeFromCodeCoverage]
public static class ApplicationContextFactoryExtensions
{
    public static Task<ApplicationContext> GetContextAsync(this IDbContextFactory<ApplicationContext> inFactory) => 
        inFactory.CreateDbContextAsync();

    public static DbSet<TDbType> GetQueryable<TDbType>(ApplicationContext inContext) where TDbType : class, IGuidEntity => 
        inContext.Set<TDbType>();

    public static async Task<DbSet<TDbType>> GetQueryableAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory) where TDbType : class, IGuidEntity => 
        GetQueryable<TDbType>(await GetContextAsync(inFactory));

    public static async Task<IQueryable<TDbType>> GetQueryableAsNoTrackingAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory) where TDbType : class, IGuidEntity => 
        (await GetQueryableAsync<TDbType>(inFactory)).AsNoTracking();

    public static async Task<IQueryable<TDbType>> GetQueryableAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity
    {
        var queryable = await GetQueryableAsync<TDbType>(inFactory);
        return inGetQueryable.Invoke(queryable);
    }
    public static async Task<IQueryable<TDbType>> GetQueryableAsNoTrackingAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity
    {
        var queryable = await GetQueryableAsNoTrackingAsync<TDbType>(inFactory);
        return inGetQueryable.Invoke(queryable);
    }
    
    public static async Task<IEnumerable<TDbType>> GetAllAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity => 
        await (await GetQueryableAsNoTrackingAsync(inFactory, inGetQueryable)).ToListAsync();

    public static Task<IEnumerable<TDbType>> GetAllAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory) where TDbType : class, IGuidEntity => 
        GetAllAsync<TDbType>(inFactory, inQueryable => inQueryable);

    public static async Task<IEnumerable<TDomainType>> GetAllAsync<TDbType, TDomainType>(this IDbContextFactory<ApplicationContext> inFactory, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity =>
        await (await GetQueryableAsNoTrackingAsync(inFactory, inGetQueryable))
            .ProjectToType<TDomainType>()
            .ToListAsync();

    public static Task<IEnumerable<TDomainType>> GetAllAsync<TDbType, TDomainType>(this IDbContextFactory<ApplicationContext> inFactory) where TDbType : class, IGuidEntity => 
        GetAllAsync<TDbType, TDomainType>(inFactory, inQueryable => inQueryable);

    public static async Task<Option<TDbType>> GetSingleAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity => 
        Optional(
            await (await GetQueryableAsNoTrackingAsync(inFactory, inGetQueryable))
                .SingleOrDefaultAsync(inDbType => inDbType.Id == inId));
    
    public static async Task<Option<TDbType>> GetSingleAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId) where TDbType : class, IGuidEntity => 
        await GetSingleAsync<TDbType>(inFactory, inId, inQueryable => inQueryable);
    
    public static async Task<Option<TDomainType>> GetSingleAsync<TDbType, TDomainType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity =>
        (await GetSingleAsync(inFactory, inId, inGetQueryable))
        .Map<TDomainType>(inSome => inSome.Adapt<TDomainType>());

    public static Task<Option<TDomainType>> GetSingleAsync<TDbType, TDomainType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId) where TDbType : class, IGuidEntity => 
        GetSingleAsync<TDbType, TDomainType>(inFactory, inId, inQueryable => inQueryable);

    public static async Task<bool> AnyAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId) where TDbType : class, IGuidEntity =>
        await (await GetQueryableAsNoTrackingAsync<TDbType>(inFactory))
            .AnyAsync(inDbType => inDbType.Id == inId);

    public static async Task DeleteAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId) where TDbType : class, IGuidEntity =>
        await (await GetQueryableAsNoTrackingAsync<TDbType>(inFactory))
            .Where(inDbType => inDbType.Id == inId)
            .ExecuteDeleteAsync();

    public static Task SaveAsync<TDbType, TDomainType>(this IDbContextFactory<ApplicationContext> inFactory, TDomainType inToSave) where TDbType : class, IGuidEntity => 
        SaveAsync(inFactory, inToSave.Adapt<TDbType>());

    public static async Task SaveAsync<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, TDbType inToSave) where TDbType : class, IGuidEntity
    {
        var context = await GetContextAsync(inFactory);
        var queryable = GetQueryable<TDbType>(context);

        if (await AnyAsync<TDbType>(inFactory, inToSave.Id))
        {
            queryable.Update(inToSave);
        }
        else
        {
            await queryable.AddAsync(inToSave);
        }

        await context.SaveChangesAsync();
    }

    public static async Task<bool> CanBeDeleted<TDbType>(this IDbContextFactory<ApplicationContext> inFactory, Guid inId) where TDbType : class, IGuidEntity
    {
        var toValidate = await GetSingleAsync<TDbType>(inFactory, inId);
        
        var context = await GetContextAsync(inFactory);
        await context.Database.BeginTransactionAsync();
        context.Remove(toValidate);
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
    }
}