using AutoFixture;
using LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using Moq;

namespace LRSchoolV2.Application.Tests.Common.SchoolYears.DeleteSchoolYear;

public class DeleteSchoolYearHandlerTests
{
    private readonly IFixture _fixture;
    private readonly DeleteSchoolYearHandler _handler;
    private readonly Mock<ISchoolYearsRepository> _mockRepository;

    public DeleteSchoolYearHandlerTests()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<ISchoolYearsRepository>();
        _handler = new DeleteSchoolYearHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteSchoolYear()
    {
        // Arrange
        var command = _fixture.Create<DeleteSchoolYearCommand>();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(inRepository => inRepository.DeleteSchoolYearAsync(It.IsAny<Guid>()), Times.Once);
    }
}