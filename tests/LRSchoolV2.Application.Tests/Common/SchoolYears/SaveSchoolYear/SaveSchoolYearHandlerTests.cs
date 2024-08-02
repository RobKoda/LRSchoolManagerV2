using AutoFixture;
using LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Domain.Common;
using Moq;

namespace LRSchoolV2.Application.Tests.Common.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearHandlerTests
{
    private readonly IFixture _fixture;
    private readonly SaveSchoolYearHandler _handler;
    private readonly Mock<ISchoolYearsRepository> _mockRepository;

    public SaveSchoolYearHandlerTests()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<ISchoolYearsRepository>();
        _handler = new SaveSchoolYearHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldSaveSchoolYear()
    {
        // Arrange
        var command = _fixture.Create<SaveSchoolYearCommand>();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(inRepository => inRepository.SaveSchoolYearAsync(It.IsAny<SchoolYear>()), Times.Once);
    }
}