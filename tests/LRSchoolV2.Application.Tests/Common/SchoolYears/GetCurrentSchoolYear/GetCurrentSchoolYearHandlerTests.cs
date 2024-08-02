using AutoFixture;
using FluentAssertions;
using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;
using Moq;

namespace LRSchoolV2.Application.Tests.Common.SchoolYears.GetCurrentSchoolYear;

public class GetCurrentSchoolYearHandlerTests
{
    private readonly IFixture _fixture;
    private readonly GetCurrentSchoolYearHandler _handler;
    private readonly Mock<ISchoolYearsRepository> _mockRepository;

    public GetCurrentSchoolYearHandlerTests()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<ISchoolYearsRepository>();
        _handler = new GetCurrentSchoolYearHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldGetCurrentSchoolYear()
    {
        // Arrange
        var query = _fixture.Create<GetCurrentSchoolYearQuery>();
        var schoolYear = _fixture.Create<SchoolYear>();
        _mockRepository.Setup(inRepository => inRepository.GetCurrentSchoolYearAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(schoolYear);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.SchoolYear.Case.Should().Be(schoolYear);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnNone_GivenNoMatch()
    {
        // Arrange
        var query = _fixture.Create<GetCurrentSchoolYearQuery>();
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.SchoolYear.IsNone.Should().BeTrue();
    }
}