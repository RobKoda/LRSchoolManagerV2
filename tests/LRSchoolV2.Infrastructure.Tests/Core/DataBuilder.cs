using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

// ReSharper disable MemberCanBePrivate.Global - Core methods

namespace LRSchoolV2.Infrastructure.Tests.Core;

public class DataBuilder<TDbContext> where TDbContext : DbContext
{
    private readonly Mock<IDbContextFactory<TDbContext>> _mockFactory;
    
    private DataBuilder(TDbContext inContext)
    {
        
        Context = inContext;
        _mockFactory = new Mock<IDbContextFactory<TDbContext>>();
        _mockFactory.Setup(inDbContextFactory => inDbContextFactory.CreateDbContext())
            .Returns(() => Context);
        _mockFactory.Setup(inDbContextFactory => inDbContextFactory.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => Context);
    }
    
    public IDbContextFactory<TDbContext> Factory => _mockFactory.Object;
    
    public TDbContext Context { get; }

    public async Task CommitAsync()
    {
        await Context.SaveChangesAsync();
        ClearTracking();
    }

    public void ClearTracking() => Context.ChangeTracker.Clear(); 
    
    public static DataBuilder<TDbContext> Build()
    {
        return new DataBuilder<TDbContext>(CreateContext());
    }
    
    public DataBuilder<TDbContext> WithEntity<T>(T inEntity)
        where T : class
    {
        Context.Add(inEntity);
        return this;
    }

    public DataBuilder<TDbContext> WithEntities<T>(IEnumerable<T> inEntities)
        where T : class =>
            inEntities.Aggregate(this, (inCurrent, inEntity) => inCurrent.WithEntity(inEntity));
    
    private static TDbContext CreateContext()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<DataBuilder<TDbContext>>()
            .Build();
        
        var options = new DbContextOptionsBuilder<TDbContext>()
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging()
            .Options;
        var context = (TDbContext) Activator.CreateInstance(typeof(TDbContext), options)!;
        context.Database.EnsureCreated();
        return context;
    }
}