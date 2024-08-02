using System.Diagnostics.CodeAnalysis;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using Microsoft.EntityFrameworkCore;

// ReSharper disable ReturnTypeCanBeEnumerable.Global - EF Core requirement
namespace LRSchoolV2.Infrastructure;

[ExcludeFromCodeCoverage]
public class ApplicationContext(DbContextOptions inOptions) : DbContext(inOptions)
{
    public DbSet<SchoolYearDataModel> SchoolYears => Set<SchoolYearDataModel>();
    
    protected override void OnModelCreating(ModelBuilder inModelBuilder)
    {
        base.OnModelCreating(inModelBuilder);

        foreach (var relationship in inModelBuilder.Model.GetEntityTypes()
                     .SelectMany(inMutableEntityType => inMutableEntityType.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder inConfigurationBuilder)
    {
        base.ConfigureConventions(inConfigurationBuilder);
        inConfigurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        inConfigurationBuilder.Properties<decimal?>().HavePrecision(18, 2);
    }
}