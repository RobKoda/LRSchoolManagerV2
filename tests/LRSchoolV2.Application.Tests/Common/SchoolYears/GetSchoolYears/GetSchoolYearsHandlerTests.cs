using AutoFixture;
using FluentAssertions;
using LRSchoolV2.Application.Common.SchoolYears.GetSchoolYears;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Tests.Core;
using Moq;

namespace LRSchoolV2.Application.Tests.Common.SchoolYears.GetSchoolYears;

public class GetSchoolYearsHandlerTests
{
    private readonly IFixture _fixture;
    private readonly GetSchoolYearsHandler _handler;
    private readonly Mock<ISchoolYearsRepository> _mockRepository;
    
    public GetSchoolYearsHandlerTests()
    {
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _mockRepository = new Mock<ISchoolYearsRepository>();
        _handler = new GetSchoolYearsHandler(_mockRepository.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldGetSchoolYears()
    {
        // Arrange
        var query = _fixture.Create<GetSchoolYearsQuery>();
        var schoolYears = _fixture.CreateMany<SchoolYear>().ToList();
        _mockRepository.Setup(inRepository => inRepository.GetSchoolYearsAsync())
            .ReturnsAsync(schoolYears);
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.SchoolYears.Should().BeEquivalentTo(schoolYears);
    }
}