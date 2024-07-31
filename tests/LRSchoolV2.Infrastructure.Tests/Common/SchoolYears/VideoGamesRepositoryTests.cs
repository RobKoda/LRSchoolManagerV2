using AutoFixture;
using FluentAssertions;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Tests.Core;
using Mapster;

namespace LRSchoolV2.Infrastructure.Tests.Common.SchoolYears;

[Collection("Sequential")]
public class SchoolYearsRepositoryTests : IDisposable
{
    private readonly DataBuilder<ApplicationContext> _dataBuilder;
    private readonly IFixture _fixture;
    private readonly SchoolYearsRepository _repository;
    
    public SchoolYearsRepositoryTests()
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ServicesRegistration).Assembly);
        
        _dataBuilder = DataBuilder<ApplicationContext>.Build();
        _repository = new SchoolYearsRepository(_dataBuilder.Factory);
        _fixture = FixtureBuilder.GetFixture();
    }
    
    public void Dispose()
    {
        _dataBuilder.Context.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }
    
    [Fact]
    public async Task GetSchoolYearsAsync_ShouldReturnSchoolYears()
    {
        // Arrange
        var schoolYears = _fixture.CreateMany<SchoolYearDataModel>();
        await _dataBuilder.WithEntities(schoolYears).CommitAsync();
        
        // Act
        var result = await _repository.GetSchoolYearsAsync();
        
        // Assert
        result.Should().BeEquivalentTo(schoolYears.Adapt<IEnumerable<SchoolYear>>());
    }
}