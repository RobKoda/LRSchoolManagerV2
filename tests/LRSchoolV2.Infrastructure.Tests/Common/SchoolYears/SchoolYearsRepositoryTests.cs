using AutoFixture;
using FluentAssertions;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Tests.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

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
    
    private SchoolYearDataModel GetSchoolYearDataModel(int inYear) =>
        _fixture.Build<SchoolYearDataModel>()
            .With(inSchoolYear => inSchoolYear.StartDate, new DateTime(inYear, 1, 1))
            .With(inSchoolYear => inSchoolYear.EndDate, new DateTime(inYear, 12, 31))
            .Create();
    
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
    
    [Fact]
    public async Task GetCurrentSchoolYearAsync_ShouldReturnNone_GivenNoMatchFromEndDate()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        var dateTime = schoolYear.EndDate.AddDays(1);
        
        // Act
        var result = await _repository.GetCurrentSchoolYearAsync(dateTime);
        
        // Assert
        result.IsNone.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetCurrentSchoolYearAsync_ShouldReturnCurrentSchoolYear_GivenMatchFromEndDate()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        var dateTime = schoolYear.EndDate;
        
        // Act
        var result = await _repository.GetCurrentSchoolYearAsync(dateTime);
        
        // Assert
        result.Case.Should().BeEquivalentTo(schoolYear);
    }
    
    [Fact]
    public async Task GetCurrentSchoolYearAsync_ShouldReturnNone_GivenNoMatchFromStartDate()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        var dateTime = schoolYear.StartDate.AddDays(-1);
        
        // Act
        var result = await _repository.GetCurrentSchoolYearAsync(dateTime);
        
        // Assert
        result.IsNone.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetCurrentSchoolYearAsync_ShouldReturnCurrentSchoolYear_GivenMatchStartEndDate()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        var dateTime = schoolYear.StartDate;
        
        // Act
        var result = await _repository.GetCurrentSchoolYearAsync(dateTime);
        
        // Assert
        result.Case.Should().BeEquivalentTo(schoolYear);
    }
    
    [Fact]
    public async Task GetPreviousSchoolYearAsync_ShouldReturnNone_GivenNoMatch()
    {
        // Arrange
        var firstSchoolYear = GetSchoolYearDataModel(2000);
        var secondSchoolYear = GetSchoolYearDataModel(2001);
        var lastSchoolYear = GetSchoolYearDataModel(2002);
        var schoolYears = new List<SchoolYearDataModel>
        {
            firstSchoolYear,
            secondSchoolYear,
            lastSchoolYear
        };
        await _dataBuilder.WithEntities(schoolYears).CommitAsync();
        
        // Act
        var result = await _repository.GetPreviousSchoolYearAsync(firstSchoolYear.Id);
        
        // Assert
        result.IsNone.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetPreviousSchoolYearAsync_ShouldReturnPreviousSchoolYear()
    {
        // Arrange
        var firstSchoolYear = GetSchoolYearDataModel(2000);
        var secondSchoolYear = GetSchoolYearDataModel(2001);
        var lastSchoolYear = GetSchoolYearDataModel(2002);
        var schoolYears = new List<SchoolYearDataModel>
        {
            firstSchoolYear,
            secondSchoolYear,
            lastSchoolYear
        };
        await _dataBuilder.WithEntities(schoolYears).CommitAsync();
        
        // Act
        var result = await _repository.GetPreviousSchoolYearAsync(lastSchoolYear.Id);
        
        // Assert
        result.Case.Should().BeEquivalentTo(secondSchoolYear.Adapt<SchoolYear>());
    }
    
    [Fact]
    public async Task GetNextSchoolYearAsync_ShouldReturnNone_GivenNoMatch()
    {
        // Arrange
        var firstSchoolYear = GetSchoolYearDataModel(2000);
        var secondSchoolYear = GetSchoolYearDataModel(2001);
        var lastSchoolYear = GetSchoolYearDataModel(2002);
        var schoolYears = new List<SchoolYearDataModel>
        {
            firstSchoolYear,
            secondSchoolYear,
            lastSchoolYear
        };
        await _dataBuilder.WithEntities(schoolYears).CommitAsync();
        
        // Act
        var result = await _repository.GetNextSchoolYearAsync(lastSchoolYear.Id);
        
        // Assert
        result.IsNone.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetNextSchoolYearAsync_ShouldReturnNextSchoolYear()
    {
        // Arrange
        var firstSchoolYear = GetSchoolYearDataModel(2000);
        var secondSchoolYear = GetSchoolYearDataModel(2001);
        var lastSchoolYear = GetSchoolYearDataModel(2002);
        var schoolYears = new List<SchoolYearDataModel>
        {
            firstSchoolYear,
            secondSchoolYear,
            lastSchoolYear
        };
        await _dataBuilder.WithEntities(schoolYears).CommitAsync();
        
        // Act
        var result = await _repository.GetNextSchoolYearAsync(firstSchoolYear.Id);
        
        // Assert
        result.Case.Should().BeEquivalentTo(secondSchoolYear.Adapt<SchoolYear>());
    }
    
    [Fact]
    public async Task AnySchoolYearAsync_ShouldReturnFalse_GivenNoMatch()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        
        // Act
        var result = await _repository.AnySchoolYearAsync(Guid.NewGuid());
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task AnySchoolYearAsync_ShouldReturnTrue_GivenMatch()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        
        // Act
        var result = await _repository.AnySchoolYearAsync(schoolYear.Id);
        
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task DeleteSchoolYearsAsync_ShouldDeleteSchoolYears()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(schoolYear).CommitAsync();
        
        // Act
        await _repository.DeleteSchoolYearAsync(schoolYear.Id);
        
        // Assert
        (await _dataBuilder.Context.SchoolYears.CountAsync()).Should().Be(0);
    }
    
    [Fact]
    public async Task SaveSchoolYearAsync_ShouldAddSchoolYearInDatabase_GivenSchoolYearNotInDatabase()
    {
        // Arrange
        var schoolYear = _fixture.Create<SchoolYearDataModel>();
        
        // Act
        await _repository.SaveSchoolYearAsync(schoolYear.Adapt<SchoolYear>());
        
        // Assert
        var schoolYearInDatabase = await _dataBuilder.Context.SchoolYears
            .AsNoTracking().SingleAsync();
        schoolYear.Should().BeEquivalentTo(schoolYearInDatabase);
    }
    
    [Fact]
    public async Task SaveSchoolYearAsync_ShouldUpdateSchoolYearInDatabase_GivenSchoolYearInDatabase()
    {
        // Arrange
        var existingSchoolYear = _fixture.Create<SchoolYearDataModel>();
        await _dataBuilder.WithEntity(existingSchoolYear).CommitAsync();
        existingSchoolYear.MembershipFee = _fixture.Create<decimal>();
        
        // Act
        await _repository.SaveSchoolYearAsync(existingSchoolYear.Adapt<SchoolYear>());
        
        // Assert
        var updatedSchoolYear = await _dataBuilder.Context.SchoolYears.SingleAsync();
        updatedSchoolYear.MembershipFee.Should().Be(existingSchoolYear.MembershipFee);
    }
}